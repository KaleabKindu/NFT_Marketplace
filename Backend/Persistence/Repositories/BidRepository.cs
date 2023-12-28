using Domain;
using Application.Contracts.Persistance;

namespace Persistence.Repositories
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public  BidRepository(AppDbContext dbContext): base(dbContext)
        {}
    }
}