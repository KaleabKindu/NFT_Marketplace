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
            return HandleResult(await Mediator.Send(new CreateCategoryCommand { Category = categoryDto }),  "Category Created Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoryQuery()),  "Category Fetched Successfully");
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategory(int Id)
        {
            return HandleResult(await Mediator.Send(new GetCategoryByIdQuery { Id = Id }), "Category Details Fetched Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category)
        {
            return  HandleResult(await Mediator.Send(new UpdateCategoryCommand { Category = category }), "Bid Updated Successfully");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActvity(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteCategoryCommand { Id = Id }),  "Bid Deleted Successfully");
        }
    }
}
