using Application.Features.Bids.Commands;
using Application.Features.Bids.Dtos;
using Application.Features.Bids.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BidsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBids([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetBidsQuery() { PageNumber = PageNumber, PageSize = PageSize}));
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBid(int Id)
        {
            return HandleResult(await Mediator.Send(new GetBidByIdQuery { Id = Id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] CreateBidDto Bid)
        {
            return  HandleResult(await Mediator.Send(new CreateBidCommand { Bid = Bid }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBid([FromBody] UpdateBidDto Bid)
        {
            return  HandleResult(await Mediator.Send(new UpdateBidCommand { Bid = Bid }));
        }

      
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBid(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteBidCommand { Id = Id }));
        }
    }
}