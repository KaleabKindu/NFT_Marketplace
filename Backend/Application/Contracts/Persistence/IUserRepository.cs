using Application.Features.Auth.Dtos;
using Application.Responses;
using Domain;
using ErrorOr;

namespace Application.Contracts.Persistance;

public interface IUserRepository
{
	Task<AppUser> CreateOrFetchUserAsync(string address);
	Task<List<AppRole>> GetUserRolesAsync(AppUser user);
	Task<PaginatedResponse<AppUser>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10, string? address = null);
	Task DeleteUserAsync(string address);
	Task<bool> AddressExists(string address);
	Task<AppUser> GetUserByAddress(string address);
	Task<ErrorOr<TokenDto>> AuthenticateUserAsync(string address, string signedNonce);
	Task<PaginatedResponse<AppUser>> GetFollowingsAsync(string address, int pageNumber = 1, int pageSize = 10);
	Task<PaginatedResponse<AppUser>> GetFollowersAsync(string address, int pageNumber = 1, int pageSize = 10);
	Task<List<AppUser>> GetAllFollowersAsync(string address);
	Task<bool> IsFollowing(string address, string targetAddress);
	Task UpdateVolume(string Id, double volume, int increateSellCount = 0);
	Task<bool> CreateNetwork(string follower, string followee);
	Task<bool> RemoveNetwork(string follower, string followee);
}