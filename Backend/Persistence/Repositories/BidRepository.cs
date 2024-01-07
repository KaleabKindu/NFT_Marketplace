using Application.Contracts.Persistance;
using Domain.Bids;

namespace Persistence.Repositories
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public  BidRepository(AppDbContext dbContext): base(dbContext)
        {}
    }
}