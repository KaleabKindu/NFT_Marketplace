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
                Profile = new UserProfile() { UserName = _faker.Internet.UserName() }
            };

            var result = await _userManager.CreateAsync(user);
            var authorizationResult = await _userManager.AddToRoleAsync(user, "Trader");

            if (!result.Succeeded || !authorizationResult.Succeeded){
                throw new DbAccessException($"Unable to save user to database:{ result.Errors.ToArray()[0]}");
            }

            return user;
        }        

        public async Task<List<AppRole>> GetUserRolesAsync(AppUser user)
        {
            return (await _userManager.GetRolesAsync(user)).Select(role => new AppRole { Name = role }).ToList();
        }

        public async Task<PaginatedResponse<AppUser>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userManager.Users
                .Include(user => user.Profile)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResponse<AppUser>
            {
                Value = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = await _userManager.Users.CountAsync()
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
            if(UpdateResult == 0)
            {
                throw new DbAccessException($"Unable to update user nonce");
            }

            var tokenInfo = _jwtService.GenerateToken(user, roles);
            return new TokenDto
            {
                AccessToken = tokenInfo.Item1,
                ExpireInDays = Math.Round(tokenInfo.Item2 / (60 * 24), 2)
            };
        }

        public async  Task<AppUser> GetUserByAddress(string address)
        {
            var user = await _userManager.Users
                .Include(user => user.Profile)
                .FirstOrDefaultAsync(u => u.Address == address);
            return user;            
        }

        public async Task<PaginatedResponse<AppUser>> GetFollowersAsync(string address, int pageNumber = 1, int pageSize = 10)
        {
            var user = await _userManager.Users
                .Where(user => user.Address == address)
                .Include(user => user.Profile)
                .FirstOrDefaultAsync();

            var query = _userManager.Users
                .Where(user => user.Profile.Followers.Contains(user.Address));

            var followers = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<AppUser>
            {
                Value = followers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = await query.CountAsync()
            };
        }

        public async Task<PaginatedResponse<AppUser>> GetFollowingsAsync(string address, int pageNumber = 1, int pageSize = 10)
        {
            var query = _userManager.Users
                .Include(user => user.Profile)
                .Where(user => user.Profile.Followers.Contains(address));
            
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
                .FirstOrDefaultAsync();

            return user.Profile.Followers.Contains(address);
        }

        public async Task UpdateVolume(string Id, double sale)
        {
            var user = await _userManager.Users
                .Where(user => user.Id == Id)
                .Include(user => user.Profile)
                .FirstOrDefaultAsync();

            user.Profile.Volume += sale;
        }
    }
}
