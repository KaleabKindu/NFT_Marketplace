using Bogus;
using Domain;
using ErrorOr;
using System.Data;
using Application.Responses;
using Application.Common.Errors;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Services;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;


namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IEthereumCryptoService _ethereumService;
        private static readonly Faker _faker = new();

        public UserRepository(
            AppDbContext dbContext,
            UserManager<AppUser> userManager,
            IJwtService jwtService,
            IEthereumCryptoService ethereumService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _jwtService = jwtService;
            _ethereumService = ethereumService;
        }

        public async Task<AppUser> CreateOrFetchUserAsync(string address)
        {
            var existing_user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Address == address);
            if (existing_user != null)
                return existing_user;

            var user = new AppUser
            {
                Address = address,
                Nonce = Guid.NewGuid().ToString(),
                Profile = new UserProfile() { UserName = address.Substring(2, 5) }
            };

            var result = await _userManager.CreateAsync(user);
            var authorizationResult = await _userManager.AddToRoleAsync(user, "Trader");

            if (!result.Succeeded || !authorizationResult.Succeeded)
            {
                throw new DbAccessException($"Unable to save user to database:{result.Errors.ToArray()[0]}");
            }

            return user;
        }

        public async Task<List<AppRole>> GetUserRolesAsync(AppUser user)
        {
            return (await _userManager.GetRolesAsync(user)).Select(role => new AppRole { Name = role }).ToList();
        }

        public async Task<PaginatedResponse<AppUser>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10, string? address = null)
        {
            var users = _userManager.Users
                .Include(user => user.Profile).AsQueryable();

            if (address != null)
            {
                users = users.Where(user => user.Address != address);
            }

            var count = await users.CountAsync();


            var usersList = await users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<AppUser>
            {
                Value = usersList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count
            };
        }

        public async Task DeleteUserAsync(string address)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Address == address);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new DbAccessException($"Unable to delete user: {result.Errors?.FirstOrDefault()?.Description}");
                }
            }
        }

        public async Task<bool> AddressExists(string address)
        {
            return await _userManager.Users.AnyAsync(u => u.Address == address);
        }

        public async Task<ErrorOr<TokenDto>> AuthenticateUserAsync(string address, string signedNonce)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Address == address);
            if (user == null)
            {
                return ErrorFactory.NotFound("User", $"User with public address `{address}` not found");
            }

            bool isSignatureValid = _ethereumService.VerifyMessage(user.Nonce, signedNonce, address);

            if (!isSignatureValid)
            {
                return ErrorFactory.BadRequestError("User", "Invalid signed message");
            }

            var roles = await _userManager.GetRolesAsync(user);
            user.Nonce = Guid.NewGuid().ToString();

            _dbContext.Entry(user).State = EntityState.Modified;

            var UpdateResult = await _dbContext.SaveChangesAsync();
            if (UpdateResult == 0)
            {
                throw new DbAccessException($"Unable to update user nonce");
            }

            var tokenInfo = _jwtService.GenerateToken(user, roles);
            return new TokenDto
            {
                Id = user.Id,
                AccessToken = tokenInfo.Item1,
                ExpireInDays = Math.Round(tokenInfo.Item2 / (60 * 24), 2)
            };
        }

        public async Task<AppUser> GetUserByAddress(string address)
        {
            var user = await _userManager.Users
                .Include(user => user.Profile)
                .FirstOrDefaultAsync(u => u.Address == address);
            return user;
        }

        public async Task<PaginatedResponse<AppUser>> GetFollowersAsync(string address, int pageNumber = 1, int pageSize = 10)
        {
            var target = await _userManager.Users
                .Where(user => user.Address == address)
                .Include(user => user.Profile)
                .ThenInclude(profile => profile.Followers)
                .FirstOrDefaultAsync();

            var count = target.Profile.Followers.Count;

            var followers = target.Profile.Followers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedResponse<AppUser>
            {
                Value = followers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count
            };
        }

        public async Task<List<AppUser>> GetAllFollowersAsync(string address)
        {
            var target = await _userManager.Users
                .Where(user => user.Address == address)
                .Include(user => user.Profile)
                .ThenInclude(profile => profile.Followers)
                .FirstOrDefaultAsync();

            var count = target.Profile.Followers.Count;

            return target.Profile.Followers;
        }

        public async Task<PaginatedResponse<AppUser>> GetFollowingsAsync(string address, int pageNumber = 1, int pageSize = 10)
        {
            var followerUser = await _userManager.Users.SingleOrDefaultAsync(user => user.Address == address);

            var query = _userManager.Users
                .Include(user => user.Profile)
                .Where(user => user.Profile.Followers.Contains(followerUser));

            var followings = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<AppUser>
            {
                Value = followings,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = await query.CountAsync()
            };
        }

        public async Task<bool> IsFollowing(string address, string targetAddress)
        {
            var user = await _userManager.Users
                .Where(user => user.Address == targetAddress)
                .Include(user => user.Profile)
                .ThenInclude(profile => profile.Followers)
                .FirstOrDefaultAsync();

            return user.Profile.Followers.FirstOrDefault(pfl => pfl.Address == address) != null;
        }

        public async Task UpdateVolume(string Id, double sale, int increaseSellCount = 0)
        {
            var user = await _userManager.Users
                .Where(user => user.Id == Id)
                .Include(user => user.Profile)
                .FirstOrDefaultAsync();
            user.Profile.TotalSalesCount += increaseSellCount;
            user.Profile.Volume += sale;
        }

        public async Task<bool> CreateNetwork(string follower, string followee)
        {
            var user = await _userManager.Users
                .Where(user => user.Address == followee)
                .Include(user => user.Profile)
                .FirstOrDefaultAsync();
            if (user.Profile.Followers.FirstOrDefault(usr => usr.Address == follower) != null)
                return false;

            var followerUser = await _userManager.Users.SingleOrDefaultAsync(user => user.Address == follower);
            user.Profile.Followers.Add(followerUser);
            return true;
        }

        public async Task<bool> RemoveNetwork(string follower, string followee)
        {
            var user = await _userManager.Users
                .Where(user => user.Address == followee)
                .Include(user => user.Profile)
                .FirstOrDefaultAsync();

            if (user.Profile.Followers.FirstOrDefault(usr => usr.Address == follower) == null)
                return false;

            var followerUser = await _userManager.Users.SingleOrDefaultAsync(user => user.Address == follower);
            user.Profile.Followers.Remove(followerUser);
            return true;
        }
    }
}
