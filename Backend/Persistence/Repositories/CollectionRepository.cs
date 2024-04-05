using Application.Contracts.Persistance;
using Domain.Assets;
using Domain.Collections;
using Microsoft.EntityFrameworkCore;
using ErrorOr;

namespace Persistence.Repositories
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Collection>> GetAllAsync(string Category, double MinFloorPrice, double MaxFloorPrice, string CreatorAddress, int page=1, int limit=10)
        {
            int skip = (page - 1) * limit;
        
            return await _dbContext.Set<Collection>()
                .Include(entity => entity.Creator)
                .Where(entity => entity.Creator.Address == CreatorAddress)
                .Where(entity => entity.Category == Category)
                .Where(entity => entity.FloorPrice >= MinFloorPrice && entity.FloorPrice <= MaxFloorPrice)
                .OrderByDescending(entity => entity.CreatedAt)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }
        
        public async  Task<ErrorOr<Tuple<int,IEnumerable<Collection>>>> GetTrendingAsync(int page=1, int limit=10)
        {
            int skip = (page - 1) * limit;
            var thresholdDateTime = DateTime.UtcNow.AddHours(-24);
        
            var collections =  _dbContext.Collections
                .Include(entity => entity.Creator)
                .Include( cl => cl.Assets.Where(x => x.Status == AssetStatus.OnSale))
                .ThenInclude(ast => ast.Bids.Where(bd => bd.CreatedAt > thresholdDateTime  ))
                .Include( cl => cl.Assets.Where(x => x.Status == AssetStatus.OnSale))
                .ThenInclude( ast => ast.Auction)
                .OrderByDescending(entity => entity.Assets.Sum(ast => ast.Auction == null? ast.Bids.Count : ast.CreatedAt > thresholdDateTime? 1:0 ))
                .AsQueryable();
            
            var count = await collections.CountAsync();

            var collectionsList = await collections.Skip(skip)
                .Take(limit)
                .ToListAsync();

            return new Tuple<int, IEnumerable<Collection>>(count,collectionsList);

        }

        public async Task<int> CountAsync(string Category, double MinFloorPrice, double MaxFloorPrice, string CreatorAddress){
            return await _dbContext.Set<Collection>()
                .Include(entity => entity.Creator)
                .Where(entity => entity.Creator.Address == CreatorAddress)
                .Where(entity => entity.Category == Category)
                .Where(entity => entity.FloorPrice >= MinFloorPrice && entity.FloorPrice <= MaxFloorPrice)
                .OrderByDescending(entity => entity.CreatedAt)
                .CountAsync();
        }
    }
}