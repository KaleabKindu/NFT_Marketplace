using Application.Features.Bids.Dtos;
using Domain.Bids;
using ErrorOr;
namespace Application.Contracts.Persistance
{
    public interface IBidRepository : IRepository<Bid>
    {

        Task<ErrorOr<Tuple<int, List<BidsListDto>>>> GetAllBidsAsync(int assetId, int page = 1, int limit = 10);

        Task<Bid> GetBidByIdAsync(long id);

        Task<int> Count(int AssetId);
    }
}