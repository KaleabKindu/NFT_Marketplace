using Application.Features.Auth.Dtos;
using Application.Responses;
using Domain;

namespace Application.Contracts.Persistance;

public interface IUserRepository
{
    // Create
	Task<AppUser> CreateUserAsync(string publicAddress);

    // Read
	Task<List<AppRole>> GetUserRolesAsync(AppUser user);
    Task<string> GetUserNonceAsync(string publicAddress);

	Task<PaginatedResponse<AppUser>> GetAllUsersAsync( int pageNumber = 1, int pageSize = 10);
	Task<AppUser?> FindUserByPublicAddressAsync(string publicAddress);

    // Delete
	Task DeleteUserAsync(string publicAddress);

    // Other
	Task<bool> PublicAddressExists(string publicAddress);
	Task<TokenDto> AuthenticateUserAsync(string publicAddress, string signedNonce);
}