using Application.Features.Bids.Commands;
using Application.Features.Bids.Dtos;
using Application.Features.Bids.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BidsController : BasaApiController
    {
        [HttpGet] //api/Bids
        public async Task<IActionResult> GetBid()
        {
            return HandleResult(await Mediator.Send(new GetBidsQuery()), "Bids fetched successfully");
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBid(int Id)
        {
            return HandleResult(await Mediator.Send(new GetBidByIdQuery { Id = Id }), "Bid details fetched successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid(CreateBidDto Bid)
        {
            return  HandleResult(await Mediator.Send(new CreateBidCommand { Bid = Bid }), "Bid created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBid(UpdateBidDto Bid)
        {
            return  HandleResult(await Mediator.Send(new UpdateBidCommand { Bid = Bid }), "Bid updated successfully");
        }

      
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActvity(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteBidCommand { Id = Id }), "Bid deleted successfully");
        }
    }
}