using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Queries;
using Application.Contracts;

namespace API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            return HandleResult(await Mediator.Send(new CreateCategoryCommand { Category = categoryDto }));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllCategoryQuery(){ PageNumber=pageNumber, PageSize=pageSize }));
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return HandleResult(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto category)
        {
            return  HandleResult(await Mediator.Send(new UpdateCategoryCommand { Category = category }));
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return  HandleResult(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}
