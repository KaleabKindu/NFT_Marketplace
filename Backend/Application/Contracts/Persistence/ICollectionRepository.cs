using Domain.Collections;

namespace Application.Contracts.Persistance
{
    public interface ICollectionRepository:IRepository<Collection>{
        Task<IEnumerable<Collection>> GetAllAsync(string Category, double MinFloorPrice, double MaxFloorPrice, string CreatorAddress, int page=1, int limit=10);
        Task<int> CountAsync(string Category, double MinFloorPrice, double MaxFloorPrice, string CreatorAddress);
    }

}