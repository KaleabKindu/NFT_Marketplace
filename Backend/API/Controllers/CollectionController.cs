using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Assets.Query;
using Microsoft.AspNetCore.Authorization;
using Domain.Assets;

namespace API.Controllers
{
    public class CollectionController : BaseController
    {
        public CollectionController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCollections([FromQuery] string Creator, [FromQuery] string Query, [FromQuery] AssetCategory Category = default, [FromQuery] double MinVolume = 0, [FromQuery] double MaxVolume = 1000.0d, [FromQuery] string SortBy = "date_added", [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllCollectionsQuery { Creator=Creator, Query=Query, Category=Category, MinVolume=MinVolume, MaxVolume=MaxVolume, SortBy=SortBy, PageNumber=PageNumber, PageSize=PageSize }));
        }
        
        [HttpGet("trending")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTrendingCollections( [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetTrendingCollectionsQuery(){ PageNumber=PageNumber, PageSize=PageSize }));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            return HandleResult(await Mediator.Send(new GetCollectionDetailsQuery { Id = id }));
        }
    }
}
