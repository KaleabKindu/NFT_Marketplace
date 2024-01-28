using Application.Contracts.Presistence;
using Domain.Assets;
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

        public async Task<IEnumerable<Asset>> GetAssetsWOpenAuct()
        {
            throw new NotImplementedException();
        }

        public async Task<Asset> GetAssetWithUser (long id){
            return  await _context.Assets
            .Include( asset => asset.Creator)
            .Include(asset => asset.Owner)
            .FirstOrDefaultAsync( asset => asset.Id == id);
        }
    }
}