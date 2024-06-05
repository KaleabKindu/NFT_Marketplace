using Application.Contracts.Persistance;
using Domain.Assets;
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

        public async Task<Tuple<int, IEnumerable<Collection>>> GetAllAsync(string CreatorAddress, string Query, AssetCategory Category, double MinVolume, double MaxVolume, string SortBy, int page = 1, int limit = 10)
        {
            int skip = (page - 1) * limit;

            var query = _dbContext.Set<Collection>()
                .Include(entity => entity.Creator)
                .Where(entity => string.IsNullOrEmpty(CreatorAddress) || entity.Creator.Address == CreatorAddress)
                .Where(entity => Regex.IsMatch(entity.Name.ToLower(), string.IsNullOrEmpty(Query) ? ".*" : Regex.Escape(Query.ToLower())))
                .Where(entity => Category == default || entity.Category == Category.ToString())
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
            var thresholdDateTime = DateTime.UtcNow.AddHours(-24);

            var collections = _dbContext.Collections
                .Include(entity => entity.Creator)
                .Include(cl => cl.Assets.Where(x => x.Status != AssetStatus.NotOnSale))
                .ThenInclude(ast => ast.Bids.Where(bd => bd.CreatedAt > thresholdDateTime))
                .Include(cl => cl.Assets.Where(x => x.Status != AssetStatus.NotOnSale))
                .ThenInclude(ast => ast.Auction)
                .OrderByDescending(entity => entity.Assets.Sum(ast => ast.Auction == null ? ast.Bids.Count : ast.CreatedAt > thresholdDateTime ? 1 : 0))
                .AsQueryable();

            var count = await collections.CountAsync();

            var collectionsList = await collections.Skip(skip)
                .Take(limit)
                .ToListAsync();

            return new Tuple<int, IEnumerable<Collection>>(count, collectionsList);

        }
    }
}