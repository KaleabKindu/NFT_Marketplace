using Application.Contracts.Persistance;
using Domain.Categories;
namespace Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}