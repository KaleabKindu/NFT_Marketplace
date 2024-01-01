using Application.Features.Auth.Dtos;
using Application.Responses;
using Domain;
using ErrorOr;

namespace Application.Contracts.Persistance;

public interface IUserRepository
{
    // Create
	Task<AppUser> CreateOrFetchUserAsync(string publicAddress);

    // Read
	Task<List<AppRole>> GetUserRolesAsync(AppUser user);
	Task<PaginatedResponse<AppUser>> GetAllUsersAsync( int pageNumber = 1, int pageSize = 10);

    // Delete
	Task DeleteUserAsync(string publicAddress);

    // Other
	Task<bool> PublicAddressExists(string publicAddress);
	Task<ErrorOr<TokenDto>> AuthenticateUserAsync(string publicAddress, string signedNonce);
}