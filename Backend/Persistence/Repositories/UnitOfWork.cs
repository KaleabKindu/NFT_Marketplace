﻿using Application.Contracts.Persistance;
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
        private UserManager<AppUser> _usermanager;
        private IAssetRepository _assetRepository;
        private IUserRepository _userRepository;
        private IBidRepository _bidRepository;
        private IAuctionRepository _AuctionRepository;
        private ICollectionRepository _CollectionRepository;
        private IProvenanceRepository _ProvenanceRepository;

        public UnitOfWork(AppDbContext dbContext, UserManager<AppUser> userManager, IJwtService jwtService, IEthereumCryptoService ethereumCryptoService,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userRepository = new UserRepository(_dbContext, userManager, jwtService, ethereumCryptoService);
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
                    _bidRepository = new BidRepository(_dbContext,_mapper);

                return _bidRepository;
            }
        }

        public IAssetRepository AssetRepository {
            get{
                if (_assetRepository == null)
                    _assetRepository =  new AssetRepository(_dbContext,_mapper);
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

        public ICollectionRepository CollectionRepository{
            get {
                if (_CollectionRepository == null)
                    _CollectionRepository = new CollectionRepository(_dbContext);
                return _CollectionRepository;
            }
        }

        public IProvenanceRepository ProvenanceRepository
        {
            get
            {
                if (_ProvenanceRepository == null)
                    _ProvenanceRepository = new ProvenanceRepository(_dbContext);
                return _ProvenanceRepository;
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
