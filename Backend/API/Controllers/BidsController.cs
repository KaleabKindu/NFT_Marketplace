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

        [HttpGet("{assetId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBids([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromRoute] int assetId = default)
        {
            return HandleResult(await Mediator.Send(new GetBidsQuery() { PageNumber = pageNumber, PageSize = pageSize, AssetId = assetId }));
        }
    }
}