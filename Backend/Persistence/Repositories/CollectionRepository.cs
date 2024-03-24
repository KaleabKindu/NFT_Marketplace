using Application.Contracts.Persistance;
using Domain.Collections;
using Microsoft.EntityFrameworkCore;

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
                .Where(entity => entity.Creator.PublicAddress == CreatorAddress)
                .Where(entity => entity.Category == Category)
                .Where(entity => entity.FloorPrice >= MinFloorPrice && entity.FloorPrice <= MaxFloorPrice)
                .OrderByDescending(entity => entity.CreatedAt)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string Category, double MinFloorPrice, double MaxFloorPrice, string CreatorAddress){
            return await _dbContext.Set<Collection>()
                .Include(entity => entity.Creator)
                .Where(entity => entity.Creator.PublicAddress == CreatorAddress)
                .Where(entity => entity.Category == Category)
                .Where(entity => entity.FloorPrice >= MinFloorPrice && entity.FloorPrice <= MaxFloorPrice)
                .OrderByDescending(entity => entity.CreatedAt)
                .CountAsync();
        }
    }
}