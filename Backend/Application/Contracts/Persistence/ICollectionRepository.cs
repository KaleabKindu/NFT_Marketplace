using Application.Features.Assets.Query;
using Domain.Assets;
using Domain.Collections;
using ErrorOr;
namespace Application.Contracts.Persistance
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Task<ErrorOr<Tuple<int, IEnumerable<Collection>>>> GetTrendingAsync(int page = 1, int limit = 10);
        Task<Tuple<int, IEnumerable<Collection>>> GetAllAsync(string CreatorAddress, string Query, double MinVolume, double MaxVolume, string SortBy, int page = 1, int limit = 10);
    }
}