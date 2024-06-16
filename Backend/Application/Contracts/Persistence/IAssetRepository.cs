using MediatR;
using ErrorOr;
using Domain.Assets;
using System.Numerics;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Application.Responses;

namespace Application.Contracts.Presistence
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAssetsWOpenAuct();
        Task<ErrorOr<AssetDetailDto>> GetAssetWithDetail(long id, string userId);
        Task<ErrorOr<Unit>> ToggleAssetLike(long assetId, string userId);
        Task<ErrorOr<Tuple<int, IEnumerable<AssetListDto>>>> GetFilteredAssets(string userId, string? query, double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creatorId, int pageNumber, int pageSize);
        Task<ErrorOr<Dictionary<string, int>>> GetCategoriesAssetCount();

        Task<ErrorOr<Tuple<int, IEnumerable<AssetListDto>>>> GetTrendingAssets(string userId, int pageNumber, int pageSize);
        Task<Asset> GetAssetByAuctionId(long auctionId);

        void DeleteAsset(Asset asset);

        Task<Asset> GetAssetByTokenId(long tokenId);
        Task<PaginatedResponse<Asset>> SemanticBasedAssetSearch(string query, int pageNumber = 1, int pageSize = 10);

        Task<ErrorOr<Tuple<int, List<AssetListDto>>>> GetOwnedAssetsAsync(string ownerId, int pageNumber, int pageSize);

        Task<ErrorOr<Tuple<int, List<AssetListDto>>>> GetCreatedAssetsAsync(string creatorId, int pageNumber, int pageSize);

        Task MarkEmbeddingUpdate(long Id);
    }
}