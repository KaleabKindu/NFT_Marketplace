using Application.Features.Assets.Query;
using Domain.Assets;
using Domain.Collections;

namespace Application.Contracts.Persistance
{
    public interface ICollectionRepository:IRepository<Collection>{
        Task<IEnumerable<Collection>> GetAllAsync(string CreatorAddress, string Query, AssetCategory Category, double MinVolume, double MaxVolume, string SortBy, int page=1, int limit=10);
        Task<int> CountAsync(string CreatorAddress, string Query, AssetCategory Category, double MinVolume, double MaxVolume);
    }

}