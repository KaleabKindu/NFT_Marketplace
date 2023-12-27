using Application.Contracts.Persistance;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IServiceProvider _services;
        private UserManager<AppUser> _usermanager;
        private IBidRepository _bidRepository;
        private IOfferRepository _offerRepository;

        public UnitOfWork(AppDbContext dbContext,IServiceProvider services)
        {
            _dbContext = dbContext;
            _services = services;
        }
                
        public UserManager<AppUser> UserManager
        {
            get
            {
                _usermanager ??= _services.GetService<UserManager<AppUser>>();
                return _usermanager;
            }
        }

        public IBidRepository BidRepository
        {
            get
            {
                if (_bidRepository == null)
                    _bidRepository = new BidRepository(_dbContext);

                return _bidRepository;
            }
        }

        public IOfferRepository OfferRepository
        {
            get {                
                _offerRepository ??= new OfferRepository(_dbContext);
                return _offerRepository;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
