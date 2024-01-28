using Domain.Bids;

namespace Application.Contracts.Persistance
{
    public interface IBidRepository:IRepository<Bid>{ 

        Task<IEnumerable<Bid>> GetAllBidsAsync( int AssetId, int page=1, int limit=10);

        Task<Bid> GetBidByIdAsync(long id);

        Task<int> Count(int AssetId);
    }
}