using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Dtos;
using Domain.Category;
using Application.Features.Categories.Queries;

namespace API.Controllers
{
    public class CategoriesController : BasaApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
        {
            return HandleResult(await Mediator.Send(new CreateCategoryCommand { Category = categoryDto }));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoryQuery()));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategory(int Id)
        {
            return HandleResult(await Mediator.Send(new GetCategoryByIdQuery { Id = Id }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category)
        {
            return  HandleResult(await Mediator.Send(new UpdateCategoryCommand { Category = category }));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActvity(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteCategoryCommand { Id = Id }));
        }
    }
}
