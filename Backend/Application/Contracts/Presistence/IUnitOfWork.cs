using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Presistence
{
    public interface IUnitOfWork:IDisposable
    {
        UserManager<AppUser> UserManager { get; }
        Task<int> Save();
    }
}
