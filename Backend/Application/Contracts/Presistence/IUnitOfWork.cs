using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Presistence
{
    public interface IUnitOfWork:IDisposable
    {
        public IAssetRepository AssetRepository{get;}
        UserManager<AppUser> UserManager { get; }
        Task<int> Save();
    }
}
