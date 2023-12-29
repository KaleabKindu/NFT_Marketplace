using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Queries;

namespace API.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            return HandleResult(await Mediator.Send(new CreateCategoryCommand { Category = categoryDto }));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllCategoryQuery(){ PageNumber=PageNumber, PageSize=PageSize }));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategory(int Id)
        {
            return HandleResult(await Mediator.Send(new GetCategoryByIdQuery { Id = Id }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto category)
        {
            return  HandleResult(await Mediator.Send(new UpdateCategoryCommand { Category = category }));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteCategoryCommand { Id = Id }));
        }
    }
}
