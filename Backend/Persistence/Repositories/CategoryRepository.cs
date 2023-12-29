using Application.Contracts.Persistance;
using Domain;
namespace Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}