using Domain.Assets;
using Domain.Collections;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Application.Contracts.Persistance;

namespace Persistence.Repositories
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Collection>> GetAllAsync(string CreatorAddress, string Query, AssetCategory Category, double MinVolume, double MaxVolume, string SortBy, int page=1, int limit=10)
        {
            int skip = (page - 1) * limit;

            var query = _dbContext.Set<Collection>()
                .Include(entity => entity.Creator)
                .Where(entity => string.IsNullOrEmpty(CreatorAddress) || entity.Creator.Address == CreatorAddress)
                .Where(entity => Regex.IsMatch(entity.Name.ToLower(), string.IsNullOrEmpty(Query) ? ".*" : Regex.Escape(Query.ToLower())))
                .Where(entity => Category == default || entity.Category == Category.ToString())
                .Where(entity => entity.Volume >= MinVolume && entity.Volume <= MaxVolume)
                .OrderByDescending(entity => entity.CreatedAt);

            if(SortBy == "low_high")
                query = query.OrderBy(entity => entity.Volume);
            else if(SortBy == "high_low")
                query = query.OrderByDescending(entity => entity.Volume);

            return await query.Skip(skip)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string CreatorAddress, string Query, AssetCategory Category, double MinVolume, double MaxVolume){
            return await _dbContext.Set<Collection>()
                .Include(entity => entity.Creator)
                .Where(entity => string.IsNullOrEmpty(CreatorAddress) || entity.Creator.Address == CreatorAddress)
                .Where(entity => Regex.IsMatch(entity.Name.ToLower(), string.IsNullOrEmpty(Query) ? ".*" : Regex.Escape(Query.ToLower())))
                .Where(entity => Category == default || entity.Category == Category.ToString())
                .Where(entity => entity.Volume >= MinVolume && entity.Volume <= MaxVolume)
                .CountAsync();
        }
    }
}