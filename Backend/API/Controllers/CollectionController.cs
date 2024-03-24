using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Assets.Query;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class CollectionController : BaseController
    {
        public CollectionController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCollections([FromQuery] string Creator, [FromQuery] string Query, [FromQuery] double MinFloorPrice, [FromQuery] double MaxFloorPrice, [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllCollectionsQuery(){ Creator=Creator, Category=Query, MinFloorPrice=MinFloorPrice, MaxFloorPrice=MaxFloorPrice, PageNumber=PageNumber, PageSize=PageSize }));
        }
    }
}
