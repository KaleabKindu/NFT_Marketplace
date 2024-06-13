using Application.Contracts.Persistance;
using Domain.Collections;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using ErrorOr;

namespace Persistence.Repositories
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Tuple<int, IEnumerable<Collection>>> GetAllAsync(string CreatorAddress, string Query, double MinVolume, double MaxVolume, string SortBy, int page = 1, int limit = 10)
        {
            int skip = (page - 1) * limit;

            var query = _dbContext.Collections
                .Include(entity => entity.Creator)
                .ThenInclude(entity => entity.Profile)
                .Where(entity => string.IsNullOrEmpty(CreatorAddress) || entity.Creator.Address == CreatorAddress)
                .Where(entity => Regex.IsMatch(entity.Name.ToLower(), string.IsNullOrEmpty(Query) ? ".*" : Regex.Escape(Query.ToLower())))
                .Where(entity => entity.Volume >= MinVolume && entity.Volume <= MaxVolume)
                .OrderByDescending(entity => entity.CreatedAt);

            if (SortBy == "low_high")
                query = query.OrderBy(entity => entity.Volume);
            else if (SortBy == "high_low")
                query = query.OrderByDescending(entity => entity.Volume);

            var count = await query.CountAsync();
            var collections = await query.Skip(skip)
                .Take(limit)
                .ToListAsync();
            return new Tuple<int, IEnumerable<Collection>>(count, collections);
        }

        public async Task<ErrorOr<Tuple<int, IEnumerable<Collection>>>> GetTrendingAsync(int page = 1, int limit = 10)
        {
            int skip = (page - 1) * limit;

            var collections = _dbContext.Collections
                .Where(cltn => cltn.Items > 4)
                .OrderByDescending(cltn => cltn.LatestPrice)
                .Include(entity => entity.Creator)
                .AsQueryable();

            var count = await collections.CountAsync();

            var collectionsList = await collections.Skip(skip)
                .Take(limit)
                .ToListAsync();

            return new Tuple<int, IEnumerable<Collection>>(count, collectionsList);

        }
    }
}