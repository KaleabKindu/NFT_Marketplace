using System.Numerics;
using System.Text.RegularExpressions;
using Application.Common.Errors;
using Application.Contracts.Presistence;
using Application.Contracts.Services;
using Application.Features.Assets.Dtos;
using Application.Responses;
using AutoMapper;
using Domain.Assets;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISemanticSearchService _semanticSearch;

        public AssetRepository(AppDbContext context, IMapper mapper, ISemanticSearchService semanticSearch) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _semanticSearch = semanticSearch;
        }

        public async Task<IEnumerable<Asset>> GetAssetsWOpenAuct()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long epochTimeInSeconds = (long)(DateTime.Now - epochStart).TotalSeconds;
            return await _dbContext.Assets.Include(asset => asset.Auction).Where(asset => asset.Auction.AuctionEnd > epochTimeInSeconds).ToListAsync();
        }

        public async Task<ErrorOr<Tuple<int, IEnumerable<AssetListDto>>>> GetTrendingAssets(string userId, int pageNumber, int pageSize)
        {
            var thresholdDateTime = DateTime.UtcNow.AddHours(-24);
            var assets = _dbContext.Assets
                .Where(x => x.Status != AssetStatus.NotOnSale && x.AuctionId != null)
                .Include(x => x.Bids.Where(bd => bd.CreatedAt > thresholdDateTime))
                .Include(x => x.Auction)
                .OrderByDescending(ast => ast.Bids.Count())
                .AsQueryable();


            var count = await assets.CountAsync();
            var assetList = await assets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var assetsInDto = _mapper.Map<List<AssetListDto>>(assetList);

            if (userId != null)
            {
                for (int i = 0; i < assetsInDto.Count(); i++)
                {
                    assetsInDto.ElementAt(i).Liked = await _context.Likes.AnyAsync(x => x.UserId == userId && x.AssetId == assetsInDto.ElementAt(i).Id);
                }
            }

            return new Tuple<int, IEnumerable<AssetListDto>>(count, assetsInDto);
        }

        public async Task<ErrorOr<Tuple<int, IEnumerable<AssetListDto>>>> GetFilteredAssets(string? userId, string? query, double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creator, int pageNumber, int pageSize)
        {
            var assets = _dbContext.Assets
                .Include(x => x.Creator)
                .Where(x => x.Status != AssetStatus.NotOnSale &&
                 (string.IsNullOrEmpty(creator) || x.Creator.Address == creator)
                 )
                .Include(x => x.Auction)
                .AsQueryable();

            if (minPrice != 0)
            {
                assets = assets.Where(asset => asset.Price >= minPrice);
            }
            if (maxPrice != 0)
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
                    assets = assets.Where(asset => asset.Status == AssetStatus.OnAuction);
                }
                else if (saleType == "fixed")
                {
                    assets = assets.Where(asset => asset.Status == AssetStatus.OnFixedSale);
                }
            }

            if (collectionId != null)
            {
                assets = assets.Where(asset => asset.CollectionId == collectionId);
            }

            IEnumerable<AssetListDto> assetsInDto;
            int count;

            if (query != null)
            {
                var regex = new Regex(Regex.Escape(query), RegexOptions.IgnoreCase);
                var assetsEnum = assets
                    .AsEnumerable()
                    .Where(asset => regex.IsMatch(asset.Name) || regex.IsMatch(asset.Description));

                assetsEnum = HandleSort(sortBy, assets);

                count = assetsEnum.Count();
                var resultEn = assetsEnum.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                assetsInDto = _mapper.Map<IEnumerable<AssetListDto>>(resultEn);
            }
            else
            {

                var sortedAssets = HandleSort(sortBy, assets);

                count = sortedAssets.Count();
                var assetList = sortedAssets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                assetsInDto = _mapper.Map<IEnumerable<AssetListDto>>(assetList);
            }


            if (userId != null)
            {
                for (int i = 0; i < assetsInDto.Count(); i++)
                {
                    assetsInDto.ElementAt(i).Liked = await _context.Likes.AnyAsync(x => x.UserId == userId && x.AssetId == assetsInDto.ElementAt(i).Id);
                }
            }

            return new Tuple<int, IEnumerable<AssetListDto>>(count, assetsInDto);
        }

        private IEnumerable<Asset> HandleSort(string sortBy, IEnumerable<Asset> assets)
        {
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
            else
            {
                assets = assets.OrderBy(asset => asset.Id);
            }

            return assets;
        }


        public async Task<ErrorOr<AssetDetailDto>> GetAssetWithDetail(long id, string? userId)
        {
            var asset = await _context.Assets
            .Include(asset => asset.Creator)
            .ThenInclude(ctr => ctr.Profile)
            .Include(asset => asset.Owner)
            .ThenInclude(owner => owner.Profile)
            .Include(asset => asset.Auction)
            .Include(asset => asset.Collection)
            .SingleOrDefaultAsync(asset => asset.Id == id);

            if (asset == null)
                return ErrorFactory.NotFound("Asset", "Asset Not Found");

            var assetDto = _mapper.Map<AssetDetailDto>(asset);

            if (userId != null)
            {
                var liked = _context.Likes.Any(x => x.UserId == userId && x.AssetId == id);
                assetDto.Liked = liked;
            }

            return assetDto;
        }

        public async Task<ErrorOr<Unit>> ToggleAssetLike(long assetId, string userId)
        {
            var asset = await _context.Assets.SingleOrDefaultAsync(x => x.Id == assetId);
            if (asset == null) return ErrorFactory.NotFound("Asset", "Asset Not Found");

            var like = await _context.Likes.SingleOrDefaultAsync(x => x.UserId == userId && x.AssetId == assetId);
            if (like != null)
            {
                _context.Likes.Remove(like);
                asset.Likes -= 1;
            }
            else
            {
                var newLike = new Like()
                {
                    UserId = userId,
                    AssetId = assetId
                };

                await _context.Likes.AddAsync(newLike);
                asset.Likes += 1;
            }

            _context.Assets.Update(asset);

            return Unit.Value;
        }

        public async Task<ErrorOr<Dictionary<string, int>>> GetCategoriesAssetCount()
        {
            var assetCounts = await _context.Assets
                    .GroupBy(a => a.Category)
                    .ToDictionaryAsync(g => g.Key.ToString(), g => g.Count());

            foreach (AssetCategory category in Enum.GetValues(typeof(AssetCategory)))
            {
                if (!assetCounts.ContainsKey(category.ToString())) assetCounts.Add(category.ToString(), 0);
            }

            return assetCounts;
        }

        public async Task<ErrorOr<Tuple<int, List<AssetListDto>>>> GetOwnedAssetsAsync(string ownerAddress, int pageNumber, int pageSize)
        {
            var assets = _dbContext.Assets
                .Include(x => x.Owner)
                .Where(x => x.Owner.Address == ownerAddress)
                .Include(x => x.Auction)
                .OrderBy(x => x.CreatedAt)
                .AsQueryable();


            var count = await assets.CountAsync();

            var assetList = await assets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Tuple<int, List<AssetListDto>>(count, _mapper.Map<List<AssetListDto>>(assetList));
        }

        public async Task<ErrorOr<Tuple<int, List<AssetListDto>>>> GetCreatedAssetsAsync(string creatorAddress, int pageNumber, int pageSize)
        {
            var assets = _dbContext.Assets
                .Include(x => x.Creator)
                .Where(x => x.Creator.Address == creatorAddress)
                .Include(x => x.Auction)
                .OrderBy(x => x.CreatedAt)
                .AsQueryable();


            var count = await assets.CountAsync();

            var assetList = await assets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Tuple<int, List<AssetListDto>>(count, _mapper.Map<List<AssetListDto>>(assetList));
        }

        public async void DeleteAsset(Asset asset)
        {

            var auction = await _context.Auctions.FirstOrDefaultAsync(x => x.TokenId == asset.TokenId);

            _context.Auctions.Remove(auction);

            var bids = await _context.Bids.Where(x => x.AssetId == asset.Id).ToListAsync();

            for (int i = 0; i < bids.Count(); i++)
            {
                _context.Bids.Remove(bids.ElementAt(i));
            }

            _context.Assets.Remove(asset);

        }


        public async Task<Asset> GetAssetByTokenId(long tokenId)
        {
            return await _context.Assets
                .Include(x => x.Owner)
                .ThenInclude(x => x.Profile)
                .FirstOrDefaultAsync(asset => asset.TokenId == tokenId);

        }

        public async Task<Asset> GetAssetByAuctionId(long auctionId)
        {
            return await _dbContext.Assets
                .Include(asset => asset.Auction)
                .Where(asset => asset.Auction.AuctionId == auctionId)
                .SingleOrDefaultAsync();
        }

        public async Task MarkEmbeddingUpdate(long Id)
        {
            var asset = await _dbContext.Assets
                .Where(asset => asset.Id == Id)
                .SingleOrDefaultAsync();

            asset.EmbeddingUpdated = false;
            _dbContext.Assets.Update(asset);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<Asset>> SemanticBasedAssetSearch(string query, int pageNumber = 1, int pageSize = 10)
        {
            var requireNewEmbedding = await _dbContext.Assets
                .Where(x => x.Status != AssetStatus.NotOnSale)
                .Where(asset => !asset.EmbeddingUpdated)
                .ToListAsync();

            var descriptions = requireNewEmbedding.Select(asset => asset.Name).ToList();
            float[][] embeddings = Array.Empty<float[]>();
            descriptions.Insert(0, query);
            embeddings = await _semanticSearch.GetEmbeddingService().GetBatchEmbeddingAsync(descriptions);
            for (int i = 1; embeddings.Length > 1 && i < embeddings.Length; i++)
            {
                requireNewEmbedding[i - 1].Embedding = embeddings[i];
                requireNewEmbedding[i - 1].EmbeddingUpdated = true;
            }
            _dbContext.Assets.UpdateRange(requireNewEmbedding);
            await _dbContext.SaveChangesAsync();

            var assets = await _dbContext.Assets.ToListAsync();
            assets = assets.OrderByDescending(asset => _semanticSearch.EmbeddingSimilarity(embeddings[0], asset.Embedding)).ToList();

            return new PaginatedResponse<Asset>
            {
                Count = await _dbContext.Assets.CountAsync(),
                Value = assets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}