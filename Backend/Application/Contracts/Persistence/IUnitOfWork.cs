using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository UserRepository { get; }
        IBidRepository BidRepository {get; }
        IOfferRepository OfferRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        public IAssetRepository AssetRepository{get;}


        Task<int> SaveAsync();
    }
}
