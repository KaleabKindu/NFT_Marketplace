using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork:IDisposable
    {
        UserManager<AppUser> UserManager { get; }
        IBidRepository BidRepository {get;}
        IOfferRepository OfferRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> SaveAsync();
    }
}
