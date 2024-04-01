using Application.Features.Auth.Dtos;
using Application.Responses;
using Domain;
using ErrorOr;

namespace Application.Contracts.Persistance;

public interface IUserRepository
{
    // Create
	Task<AppUser> CreateOrFetchUserAsync(string address);

    // Read
	Task<List<AppRole>> GetUserRolesAsync(AppUser user);
	Task<PaginatedResponse<AppUser>> GetAllUsersAsync( int pageNumber = 1, int pageSize = 10);

    // Delete
	Task DeleteUserAsync(string address);

    // Other
	Task<bool> AddressExists(string address);

	Task<AppUser> GetUserByAddress(string address);
	Task<ErrorOr<TokenDto>> AuthenticateUserAsync(string address, string signedNonce);
}