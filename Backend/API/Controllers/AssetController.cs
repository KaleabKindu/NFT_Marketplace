using System;
using System.Net;
using Application.Common.Responses;
using Application.Features.Assets.Command;
using Application.Features.Assets.Query;
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

            return HandleResult(await _mediator.Send(new GetAssetByIdQuery { Id = id }));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(await _mediator.Send(new GetAllAssetQuery { PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromForm] CreateAssetDto createAssetDto)
        {

            return HandleResult( await _mediator.Send(new CreateAssetCommand { CreateAssetDto = createAssetDto }));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAssetDto updateAssetDto)
        {

            return HandleResult(await _mediator.Send(new UpdateAssetCommand { UpdateAssetDto = updateAssetDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return HandleResult(await _mediator.Send(new DeleteAssetCommand { Id = id }));
        }

    }
}