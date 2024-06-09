using MediatR;
using ErrorOr;
using Domain.Assets;
using System.Numerics;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;

namespace Application.Contracts.Presistence
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAssetsWOpenAuct();
        Task<ErrorOr<AssetDetailDto>> GetAssetWithDetail(long id, string userId);
        Task<ErrorOr<Unit>> ToggleAssetLike(long assetId, string userId);
        Task<ErrorOr<Tuple<int, IEnumerable<AssetListDto>>>> GetFilteredAssets(string userId, string? query, double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creatorId, int pageNumber, int pageSize);
        Task<IEnumerable<Asset>> GetByAssetAsync(AssetCategory? category);

        Task<ErrorOr<Tuple<int, IEnumerable<AssetListDto>>>> GetTrendingAssets(string userId, int pageNumber, int pageSize);

        Task<ErrorOr<Asset>> SellAsset(BigInteger tokenId);

        Task<ErrorOr<Unit>> ResellAsset(ResellAssetEventDto resellAssetEventDto);

        Task<ErrorOr<Tuple<string, Asset>>> TransferAsset(TransferAssetEventDto transferAssetEventDto);
        Task<Asset> GetAssetByAuctionId(long auctionId);

        void DeleteAsset(Asset asset);

        Task<Asset> GetAssetByTokenId(BigInteger tokenId);
    }
}