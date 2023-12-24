using System.Collections.Generic;
using System.Linq;
using API.Model;
using API.Services;

namespace API.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories(int page, int pageSize);
        CategoryDto GetCategoryById(int id);
        CategoryDto CreateCategory(CategoryDto categoryDto);
        void UpdateCategory(CategoryDto categoryDto);
        void DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly List<CategoryDto> _categories = new List<CategoryDto>();
        private readonly TokenService _tokenService;

        public CategoryService(TokenService tokenService)
        {
            _tokenService = tokenService;
            // Initialize some sample categories (replace with actual data retrieval logic)
            _categories.Add(new CategoryDto { CategoryId = 1, CategoryName = "Category1" });
            _categories.Add(new CategoryDto { CategoryId = 2, CategoryName = "Category2" });
        }

        public IEnumerable<CategoryDto> GetCategories(int page, int pageSize)
        {
            var startIndex = (page - 1) * pageSize;
            return _categories.Skip(startIndex).Take(pageSize);
        }

        public CategoryDto GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(category => category.CategoryId == id);
        }

        public CategoryDto CreateCategory(CategoryDto categoryDto)
        {
            _categories.Add(categoryDto);
            return categoryDto;
        }

        public void UpdateCategory(CategoryDto categoryDto)
        {
            var existingCategory = _categories.FirstOrDefault(category => category.CategoryId == categoryDto.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = categoryDto.CategoryName;
            }
        }

        public void DeleteCategory(int id)
        {
            var categoryToRemove = _categories.FirstOrDefault(category => category.CategoryId == id);
            if (categoryToRemove != null)
            {
                _categories.Remove(categoryToRemove);
            }
        }
    }
}
