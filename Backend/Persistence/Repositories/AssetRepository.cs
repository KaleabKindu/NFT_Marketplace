using Application.Contracts.Presistence;
using Domain.Assets;

namespace Persistence.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        private readonly AppDbContext _context;

        public AssetRepository(AppDbContext context) : base(context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<Asset>> GetAssetsWOpenAuct()
        {
            throw new NotImplementedException();
        }
    }
}