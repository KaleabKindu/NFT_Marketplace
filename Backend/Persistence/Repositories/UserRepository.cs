using System.Data;
using System.Runtime.Serialization;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Application.Features.Auth.Dtos;
using Application.Responses;
using Bogus;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IEthereumCryptoService _ethereumService;
        private static readonly Faker _faker = new();

        public UserRepository(
            UserManager<AppUser> userManager,
            IJwtService jwtService,
            IEthereumCryptoService ethereumService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _ethereumService = ethereumService;
        }

        // Create
        public async Task<AppUser> CreateUserAsync(string publicAddress)
        {
            if (await _userManager.Users.AnyAsync(user => user.PublicAddress == publicAddress))
            {
                throw new DuplicateResourceException("Public address already registered");
            }

            var user = new AppUser
            {
                UserName = _faker.Internet.UserName(),
                PublicAddress = publicAddress,
                Nonce = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user);
            var authorizationResult = await _userManager.AddToRoleAsync(user, "Trader");

            if (!result.Succeeded || !authorizationResult.Succeeded){
                throw new DbAccessException($"Unable to save user to database:{ result.Errors.ToArray()[0]}");
            }

            return user;
        }

        // Read
        public async Task<List<AppRole>> GetUserRolesAsync(AppUser user)
        {
            return (await _userManager.GetRolesAsync(user)).Select(role => new AppRole { Name = role }).ToList();
        }

        public async Task<PaginatedResponse<AppUser>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userManager.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResponse<AppUser>
            {
                Value = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = await _userManager.Users.CountAsync()
            };
        }

        public async Task<AppUser?> FindUserByPublicAddressAsync(string publicAddress)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.PublicAddress == publicAddress);
        }

        public async Task<string> GetUserNonceAsync(string publicAddress){
            var user = await FindUserByPublicAddressAsync(publicAddress);
            if (user == null) 
                throw new NotFoundException("User not found");

            return user.Nonce;
        }

        // Delete
        public async Task DeleteUserAsync(string publicAddress)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PublicAddress == publicAddress);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new DbAccessException($"Unable to delete user: {result.Errors?.FirstOrDefault()?.Description}");
                }
            }
        }

        // Other
        public async Task<bool> PublicAddressExists(string publicAddress)
        {
            return await _userManager.Users.AnyAsync(u => u.PublicAddress == publicAddress);
        }

        public async Task<TokenDto> AuthenticateUserAsync(string publicAddress, string signedNonce)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PublicAddress == publicAddress);
            if (user == null)
            {
                throw new NotFoundException($"User with public address {publicAddress} not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            bool isSignatureValid = _ethereumService.VerifyMessage(user.Nonce, signedNonce, publicAddress);

            if (!isSignatureValid)
            {
                await _userManager.DeleteAsync(user);
                throw new EthereumVerificationException("Signed message verification failed");
            }

            user.Nonce = Guid.NewGuid().ToString();
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                throw new DbAccessException("Unable to update user nonce");
            }

            var tokenInfo = _jwtService.GenerateToken(user, roles);

            return new TokenDto
            {
                AccessToken = tokenInfo.Item1,
                ExpireInDays = Math.Round(tokenInfo.Item2 / (60 * 24), 2)
            };
        }
    }
}
