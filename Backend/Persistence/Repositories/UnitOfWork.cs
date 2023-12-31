using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.Services;
using Application.Contracts.Persistance;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IUserRepository _userRepository;
        private IBidRepository _bidRepository;
        private IOfferRepository _offerRepository;
        private ICategoryRepository _CategoryRepository;

        public UnitOfWork(AppDbContext dbContext, UserManager<AppUser> userManager, IJwtService jwtService, IEthereumCryptoService ethereumCryptoService)
        {
            _dbContext = dbContext;
            _userRepository = new UserRepository(userManager, jwtService, ethereumCryptoService);
        }
                
        public IUserRepository UserRepository 
        {
            get
            {
                return _userRepository;
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
