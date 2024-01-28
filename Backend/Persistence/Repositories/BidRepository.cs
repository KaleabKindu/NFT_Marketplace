using Application.Contracts.Persistance;
using Domain.Bids;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public  BidRepository(AppDbContext dbContext): base(dbContext)
        {}

        public async Task<IEnumerable<Bid>> GetAllBidsAsync( int AssetId, int page=1, int limit=10)
        {
            int skip = (page - 1) * limit;
        
            return await _dbContext.Set<Bid>()
                .OrderByDescending(entity => entity.CreatedAt)
                .Include(entity => entity.Asset)
                .Include(entity => entity.Bidder)
                .Where(entity => AssetId == default || entity.Asset.Id == AssetId)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }
        public async Task<int> Count(int AssetId){
            return await _dbContext.Set<Bid>()
                .Where(entity => AssetId == default || entity.Asset.Id == AssetId)
                .CountAsync();
        }

        public async Task<Bid> GetBidByIdAsync(long id)
        {
            return await _dbContext.Set<Bid>()
                .Include(entity => entity.Bidder)
                .Include(entity => entity.Asset)
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}