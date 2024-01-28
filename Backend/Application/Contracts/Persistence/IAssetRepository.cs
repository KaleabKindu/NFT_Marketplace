using Application.Contracts.Persistance;
using Domain.Assets;

namespace Application.Contracts.Presistence
{
    public interface IAssetRepository: IRepository<Asset>
    {
         Task<IEnumerable<Asset>> GetAssetsWOpenAuct();
    }
}