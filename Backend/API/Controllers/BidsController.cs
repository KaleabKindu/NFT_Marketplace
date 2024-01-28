using Application.Contracts;
using Application.Features.Bids.Commands;
using Application.Features.Bids.Dtos;
using Application.Features.Bids.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BidsController : BaseController
    {
        public BidsController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> GetBids([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10, [FromQuery] int AssetId = default)
        {
            return HandleResult(await Mediator.Send(new GetBidsQuery() { PageNumber = PageNumber, PageSize = PageSize, AssetId=AssetId }));
        }


        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> GetBid(int Id)
        {
            return HandleResult(await Mediator.Send(new GetBidByIdQuery { Id = Id }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> CreateBid([FromBody] CreateBidDto Bid)
        {
            return  HandleResult(await Mediator.Send(new CreateBidCommand { Bid = Bid, Bidder= _userAccessor.GetPublicAddress() }));
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> UpdateBid([FromBody] UpdateBidDto Bid)
        {
            return  HandleResult(await Mediator.Send(new UpdateBidCommand { Bid = Bid }));
        }

      
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> DeleteBid(int Id)
        {
            return  HandleResult(await Mediator.Send(new DeleteBidCommand { Id = Id }));
        }
    }
}