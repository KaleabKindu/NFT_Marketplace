using Application.Features.Assets.Query;
using Domain.Assets;
using Domain.Collections;

namespace Application.Contracts.Persistance
{
    public interface ICollectionRepository:IRepository<Collection>{
        Task<Tuple<int, IEnumerable<Collection>>> GetAllAsync(string CreatorAddress, string Query, AssetCategory Category, double MinVolume, double MaxVolume, string SortBy, int page=1, int limit=10);
    }

}