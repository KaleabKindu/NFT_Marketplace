using ErrorOr;
using Domain;

namespace Application.Contracts.Persistance;

public interface IUserProfileRepository : IRepository<UserProfile>
{
	Task<ErrorOr<UserProfile>> GetByAddressAsync(string address);
}