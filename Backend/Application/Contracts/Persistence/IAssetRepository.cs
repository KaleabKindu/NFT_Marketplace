using Application.Contracts.Persistance;
using Domain.Assets;

namespace Application.Contracts.Presistence
{
    public interface IAssetRepository: IRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAssetsWOpenAuct();
        Task<Asset> GetAssetWithDetail(long id);

        Task<Tuple<int,IEnumerable<Asset>>> GetFilteredAssets(string? query,double minPrice, double maxPrice, AssetCategory? category, string sortBy, string? saleType, long? collectionId, string? creatorId, int pageNumber, int pageSize);
    }
}