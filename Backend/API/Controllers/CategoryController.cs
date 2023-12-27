using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Dtos;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryListDto>> GetCategories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var categories = _categoryService.GetCategories(page, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryListDto> GetCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<CategoryListDto> CreateCategory([FromBody] CategoryListDto categoryDto)
        {
            var createdCategory = _categoryService.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("Mismatched category IDs");
            }

            _categoryService.UpdateCategory(categoryDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryService.DeleteCategory(id);

            return NoContent();
        }
    }
}
