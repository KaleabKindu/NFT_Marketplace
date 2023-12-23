using System;
using System.Net;
using Application.Common.Responses;
using Application.Features.Assets.CQRS.Command;
using Application.Features.Assets.CQRS.Query;
using Application.Features.Assets.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/assets")]
    public class AssetController : BasaApiController
    {
        private IMediator _mediator;
        public AssetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetAssetByIdQuery { Id = id });

            return HandleResult(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllAssetQuery { PageNumber = pageNumber, PageSize = pageSize });
            return HandleResult(result);
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromForm] CreateAssetDto createAssetDto)
        {
            var result = await _mediator.Send(new CreateAssetCommand { CreateAssetDto = createAssetDto });

            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAssetDto updateAssetDto)
        {
            var result = await _mediator.Send(new UpdateAssetCommand { UpdateAssetDto = updateAssetDto });

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAssetCommand { Id = id });

            return HandleResult(result);
        }





    }
}