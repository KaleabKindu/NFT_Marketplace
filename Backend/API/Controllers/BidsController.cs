using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Bids.Queries;
using Microsoft.AspNetCore.Authorization;

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
    }
}