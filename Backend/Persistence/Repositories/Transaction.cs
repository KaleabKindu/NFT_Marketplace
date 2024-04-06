using Application.Contracts.Persistence;
using Domain.Transactions;
using Domain;
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

            IQueryable<Transaction> query = _dbContext.Transactions.Include(x => x.Asset);
                
            if (assetId != 0)
            {
                query = query.Where(entity => entity.Asset.Id == assetId);
            }

            return await query
                    .OrderByDescending(entity => entity.CreatedAt)
                    .Skip(skip)
                    .Take(limit)
                    .Include(x => x.Buyer)
                    .Include(x => x.Seller)
                    .ToListAsync();
        }

        public async Task<IDictionary<AppUser, double>> GetCreatorSalesVolumeAsync(int page = 1, int limit = 10)
        {
            var transactions = await GetAllTransactionAsync(page, limit); 

            var creatorSalesVolume = transactions
                .Where(t => t.Status == TransactionStatus.Completed && t.Type == TransactionType.Sell)
                .GroupBy(t => t.Seller)
                .Select(g => new KeyValuePair<AppUser, double>(g.Key, g.Sum(t => t.Amount)))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return creatorSalesVolume;
        }
    }
}
