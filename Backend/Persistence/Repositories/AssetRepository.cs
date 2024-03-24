using System.Text.RegularExpressions;
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

        public async Task<Tuple<int,IEnumerable<Asset>>> GetFilteredAssets(string? query,double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creatorId, int pageNumber, int pageSize)
        {
            var assets = _dbContext.Assets
                .Include(x => x.Auction)
                .AsQueryable();

            if (minPrice != -1)
            {
                assets = assets.Where(asset => asset.Price >= minPrice);
            }
            if (maxPrice != -1)
            {
                assets = assets.Where(asset => asset.Price <= maxPrice);
            }
            if (category != null)
            {
                assets = assets.Where(asset => asset.Category == category);
            }

            if (saleType != null)
            {
                if (saleType == "auction")
                {
                    assets = assets.Where(asset => asset.Auction != null);
                }
                else if (saleType == "fixed")
                {
                    assets = assets.Where(asset => asset.Auction == null);
                }
            }

            if (collectionId != null)
            {
                assets = assets.Where(asset => asset.CollectionId == collectionId);
            }
            if (creatorId != null)
            {
                assets = assets.Where(asset => asset.Owner.Id == creatorId);
            }

            if (query != null)
            {
                var regex = new Regex(Regex.Escape(query), RegexOptions.IgnoreCase);
                var assetsEnum = assets
                    .AsEnumerable()
                    .Where(asset => regex.IsMatch(asset.Name) || regex.IsMatch(asset.Description))
                    .ToList();

                HandleSort(sortBy,assets);

                var resultEn =  assetsEnum.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return new Tuple<int, IEnumerable<Asset>>(assetsEnum.Count, resultEn);
            }

            HandleSort(sortBy,assets);
            

            var count = await assets.CountAsync();
            var result =  await assets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Tuple<int, IEnumerable<Asset>>(count, result);
        }

        private IEnumerable<Asset> HandleSort(string sortBy,IEnumerable<Asset> assets){
            if (sortBy == "date_added")
            {
                assets = assets.OrderBy(asset => asset.CreatedAt);
            }
            else if (sortBy == "low_high")
            {
                assets = assets.OrderBy(asset => asset.Price);
            }
            else if (sortBy == "high_low")
            {
                assets = assets.OrderByDescending(asset => asset.Price);
            }

            return assets;
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