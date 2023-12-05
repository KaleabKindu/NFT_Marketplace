using Application.Contracts.Presistence;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private UserManager<AppUser> _usermanager;
        private IServiceProvider _services;

        public UnitOfWork(AppDbContext dbContext,IServiceProvider services)
        {
            _dbContext = dbContext;
            _services = services;
        }
                
        public UserManager<AppUser> UserManager
        {
            get
            {
                if (_usermanager == null)
                    _usermanager = _services.GetService<UserManager<AppUser>>();

                return _usermanager;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
