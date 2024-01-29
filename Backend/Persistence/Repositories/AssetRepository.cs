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
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long epochTimeInSeconds = (long)(DateTime.Now - epochStart).TotalSeconds;
            return await _dbContext.Assets.Include(asset => asset.Auction).Where(asset => asset.Auction.AuctionEnd > epochTimeInSeconds).ToListAsync();
        }

        public async Task<Asset> GetAssetWithDetail (long id){
            return  await _context.Assets
            .Include( asset => asset.Creator)
            .Include(asset => asset.Owner)
            .Include(asset => asset.Auction)
            .FirstOrDefaultAsync( asset => asset.Id == id);
        }
    }
}