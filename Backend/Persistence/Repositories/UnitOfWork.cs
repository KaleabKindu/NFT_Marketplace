using Application.Contracts.Persistance;
using Application.Contracts.Presistence;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.Services;
using Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IServiceProvider _services;
        private UserManager<AppUser> _usermanager;
        private IAssetRepository _assetRepository;
        private IUserRepository _userRepository;
        private IBidRepository _bidRepository;
        private IOfferRepository _offerRepository;
        private ICategoryRepository _CategoryRepository;
        private ITransactionRepository _transactionRepository;
        private IAuctionRepository _AuctionRepository;

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

        public ITransactionRepository TransactionRepository
        {
            get {
                _transactionRepository ??= new TransactionRepository(_dbContext);
                return _transactionRepository;
            }
        }

        public IAssetRepository AssetRepository {
            get{
                if (_assetRepository == null)
                    _assetRepository =  new AssetRepository(_dbContext);
            return _assetRepository;
            }
        }

        public IAuctionRepository AuctionRepository{
            get {
                if (_AuctionRepository == null)
                    _AuctionRepository = new AuctionRepository(_dbContext);
                return _AuctionRepository;
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
