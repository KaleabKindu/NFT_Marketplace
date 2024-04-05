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
        [AllowAnonymous]
        public async Task<IActionResult> GetBids([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int tokenId = default)
        {
            return HandleResult(await Mediator.Send(new GetBidsQuery() { PageNumber = pageNumber, PageSize = pageSize, TokenId= tokenId }));
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
            return  HandleResult(await Mediator.Send(new CreateBidCommand { Bid = Bid, Bidder= _userAccessor.GetAddress() }));
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