using Application.Contracts.Persistance;
using Domain.Assets;
using MediatR;
using ErrorOr;
using Application.Features.Assets.Dtos;

namespace Application.Contracts.Presistence
{
    public interface IAssetRepository: IRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAssetsWOpenAuct();
        Task<ErrorOr<AssetDetailDto>> GetAssetWithDetail(long id, string userId);
        Task<ErrorOr<Unit>> ToggleAssetLike(long assetId, string userId);
        Task<ErrorOr<Tuple<int,IEnumerable<AssetListDto>>>> GetFilteredAssets(string userId,string? query,double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creatorId, int pageNumber, int pageSize);
        Task<ErrorOr<Tuple<int,IEnumerable<AssetListDto>>>> GetTrendingAssets(string userId,int pageNumber, int pageSize);
    }
}