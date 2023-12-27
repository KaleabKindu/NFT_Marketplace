using System.Collections.Generic;
using System.Linq;
using Application.Features.Categories.Dtos;

namespace Application.Features.Categories.Commands
{
    public interface ICategoryService
    {
        IEnumerable<CategoryListDto> GetCategories(int page, int pageSize);
        CategoryListDto GetCategoryById(int id);
        CategoryListDto CreateCategory(CategoryListDto categoryDto);
        void UpdateCategory(UpdateCategoryDto categoryDto);
        void DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly List<CategoryListDto> _categories = new List<CategoryListDto>();
        // private readonly TokenService _tokenService;

        public CategoryService()
        {
            // _tokenService = tokenService;
            // Initialize some sample categories (replace with actual data retrieval logic)
            _categories.Add(new CategoryListDto { CategoryId = 1, CategoryName = "Category1", Description = "Description1" });
            _categories.Add(new CategoryListDto { CategoryId = 2, CategoryName = "Category2",  Description = "Description2" });

         }

        public IEnumerable<CategoryListDto> GetCategories(int page, int pageSize)
        {
            var startIndex = (page - 1) * pageSize;
            return _categories.Skip(startIndex).Take(pageSize);
        }

        public CategoryListDto GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(category => category.CategoryId == id);
        }

        public CategoryListDto CreateCategory(CategoryListDto categoryDto)
        {
            _categories.Add(categoryDto);
            return categoryDto;
        }

        public void UpdateCategory(UpdateCategoryDto categoryDto)
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
