using System.Text.RegularExpressions;
using Application.Common.Errors;
using Application.Contracts.Presistence;
using Application.Features.Assets.Dtos;
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

        public AssetRepository(AppDbContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Asset>> GetAssetsWOpenAuct()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long epochTimeInSeconds = (long)(DateTime.Now - epochStart).TotalSeconds;
            return await _dbContext.Assets.Include(asset => asset.Auction).Where(asset => asset.Auction.AuctionEnd > epochTimeInSeconds).ToListAsync();
        }

        public async Task<ErrorOr<Tuple<int,IEnumerable<AssetListDto>>>> GetFilteredAssets(string? userId,string? query,double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creatorId, int pageNumber, int pageSize)
        {
            var assets = _dbContext.Assets
                .Include(x => x.Auction)
                // .Where( x => x.Status == AssetStatus.OnSale)
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

            IEnumerable<AssetListDto> assetsInDto;
            int count;

            if (query != null)
            {
                var regex = new Regex(Regex.Escape(query), RegexOptions.IgnoreCase);
                var assetsEnum = assets
                    .AsEnumerable()
                    .Where(asset => regex.IsMatch(asset.Name) || regex.IsMatch(asset.Description))
                    .ToList();

                HandleSort(sortBy,assets);

                count = assetsEnum.Count();
                var resultEn =  assetsEnum.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                assetsInDto = _mapper.Map<IEnumerable<AssetListDto>>(resultEn);
            }
            else{

                HandleSort(sortBy,assets);
                

                count = await assets.CountAsync();
                var assetList =  await assets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                assetsInDto = _mapper.Map<IEnumerable<AssetListDto>>(assetList);
            }


            if (userId != null){
                for (int i = 0; i < assetsInDto.Count(); i++)
                {
                    assetsInDto.ElementAt(i).Liked = await _context.Likes.AnyAsync(x => x.UserId == userId && x.AssetId == assetsInDto.ElementAt(i).Id);
                }
            }

            return new Tuple<int, IEnumerable<AssetListDto>>(count, assetsInDto);
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


        public async Task<ErrorOr<AssetDetailDto>> GetAssetWithDetail(long id, string? userId){
            var asset =  await _context.Assets
            .Include( asset => asset.Creator)
            .Include(asset => asset.Owner)
            .Include(asset => asset.Auction)
            .Include( asset => asset.Collection)
            .FirstOrDefaultAsync( asset => asset.Id == id);

            if ( asset == null ) 
                return ErrorFactory.NotFound("Asset","Asset Not Found");

            var assetDto =  _mapper.Map<AssetDetailDto>(asset);

            if (userId != null){
                var liked = _context.Likes.Any(x => x.UserId == userId && x.AssetId == id);
                assetDto.Liked = liked;
            }
            
            return assetDto;
        }

        public async Task<ErrorOr<Unit>> ToggleAssetLike(long assetId, string userId){
            var asset = await _context.Assets.SingleOrDefaultAsync(x => x.Id == assetId);
            if(asset == null) return ErrorFactory.NotFound("Asset","Asset Not Found");
            
            var like = await _context.Likes.FirstOrDefaultAsync(x => x.UserId == userId && x.AssetId == assetId);
            if (like != null){
                _context.Likes.Remove(like);
            }
            else {
                var newLike = new Like(){
                    UserId = userId,
                    AssetId = assetId
                };

                await _context.Likes.AddAsync(newLike);
            }

            await _context.SaveChangesAsync();
      
            asset.Likes = await _context.Likes.Where(x => x.AssetId == assetId).CountAsync();
            await _context.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}