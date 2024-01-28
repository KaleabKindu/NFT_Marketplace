using Application.Contracts.Persistence;
using Domain.Trasactions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionAsync(int page = 1, int limit = 10, int assetId = 0)
        {
            int skip = (page - 1) * limit;

            IQueryable<Transaction> query = _dbContext.Set<Transaction>()
                .OrderByDescending(entity => entity.CreatedAt);

            if (assetId != 0)
            {
                query = query.Where(entity => entity.AssetId == assetId);
            }

            return await query.Skip(skip).Take(limit).ToListAsync();
        }
    }
}
