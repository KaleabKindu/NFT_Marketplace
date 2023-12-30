using System;
using Application.Contracts.Presistence;
using Domain;
using Domain.Asset;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        private readonly AppDbContext _context;

        public AssetRepository(AppDbContext context) : base(context)
        {
            _context = context;
            
        }

        


        
    }
}