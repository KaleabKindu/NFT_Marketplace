using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork:IDisposable
    {
        UserManager<AppUser> UserManager { get; }

        IOfferRepository OfferRepository { get; }

        Task<int> SaveAsync();
    }
}
