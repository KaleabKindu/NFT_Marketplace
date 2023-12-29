using Application.Contracts.Persistance;
using Domain;
namespace Persistence.Repositories
{

    // IOfferRepository.cs
    public class OfferRepository : Repository<Offer>,IOfferRepository
    {
        private readonly AppDbContext _dbContext;

        public OfferRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}