using Domain;
using ErrorOr;
using Application.Common.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Persistance;

namespace Persistence.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserProfileRepository(
            AppDbContext dbContext,
            UserManager<AppUser> userManager
            ) : base(dbContext)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<UserProfile>> GetByAddressAsync(string address)
        {
            var user = await _userManager.Users
                .Include(usr => usr.Profile)
                .FirstOrDefaultAsync(usr => usr.Address == address);

            if (user == null) return ErrorFactory.NotFound("User", "User not found");

            return user.Profile;
        }
    }
}
