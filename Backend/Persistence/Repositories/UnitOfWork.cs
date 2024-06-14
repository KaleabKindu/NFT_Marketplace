using Application.Contracts.Persistance;
using Application.Contracts.Presistence;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.Services;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _services;
        private readonly UserManager<AppUser> _usermanager;
        private ISemanticSearchService _semanticSearch;
        private IAssetRepository _assetRepository;
        private IUserRepository _userRepository;
        private IBidRepository _bidRepository;
        private IAuctionRepository _AuctionRepository;
        private ICollectionRepository _CollectionRepository;
        private IProvenanceRepository _ProvenanceRepository;
        private IUserProfileRepository _UserProfileRepository;

        private INotificationRepository _notificationRepository;
        private readonly IJwtService _jwtService;
        private readonly IEthereumCryptoService _ethereumCryptoService;


        public UnitOfWork(AppDbContext dbContext, UserManager<AppUser> userManager, IJwtService jwtService, IEthereumCryptoService ethereumCryptoService, IMapper mapper, ISemanticSearchService semanticSearch)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtService = jwtService;
            _ethereumCryptoService = ethereumCryptoService;
            _usermanager = userManager;
            _semanticSearch = semanticSearch;
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_dbContext, _usermanager, _jwtService, _ethereumCryptoService);
                return _userRepository;
            }
        }

        public IBidRepository BidRepository
        {
            get
            {
                _bidRepository ??= new BidRepository(_dbContext, _mapper);

                return _bidRepository;
            }
        }

        public IAssetRepository AssetRepository
        {
            get
            {
                _assetRepository ??= new AssetRepository(_dbContext, _mapper, _semanticSearch);
                return _assetRepository;
            }
        }

        public IAuctionRepository AuctionRepository
        {
            get
            {
                _AuctionRepository ??= new AuctionRepository(_dbContext);
                return _AuctionRepository;
            }
        }

        public ICollectionRepository CollectionRepository
        {
            get
            {
                _CollectionRepository ??= new CollectionRepository(_dbContext);
                return _CollectionRepository;
            }
        }

        public IProvenanceRepository ProvenanceRepository
        {
            get
            {
                _ProvenanceRepository ??= new ProvenanceRepository(_dbContext);
                return _ProvenanceRepository;
            }
        }

        public IUserProfileRepository UserProfileRepository
        {
            get
            {
                _UserProfileRepository ??= new UserProfileRepository(_dbContext, _usermanager);
                return _UserProfileRepository;
            }
        }

        public INotificationRepository NotificationRepository
        {
            get
            {
                _notificationRepository ??= new NotificationRepository(_dbContext, _mapper);
                return _notificationRepository;
            }
        }

        public IEthereumCryptoService EthereumCryptoService { get; }

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
