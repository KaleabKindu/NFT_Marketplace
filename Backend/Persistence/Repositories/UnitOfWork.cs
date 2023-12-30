using Application.Contracts.Persistance;
using Application.Contracts.Presistence;
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
        private IAssetRepository _assetRepository;
        private IBidRepository _bidRepository;
        private IOfferRepository _offerRepository;
        private ICategoryRepository _CategoryRepository;

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

        public ICategoryRepository CategoryRepository
        {
            get {                
                _CategoryRepository ??= new CategoryRepository(_dbContext);
                return _CategoryRepository;
            }
        }

        public IAssetRepository AssetRepository {
            get{
                if (_assetRepository == null)
                    _assetRepository =  new AssetRepository(_dbContext);
            return _assetRepository;
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
