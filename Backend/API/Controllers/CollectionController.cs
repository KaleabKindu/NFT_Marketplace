using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Assets.Query;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Collections.Dtos;
using Application.Features.Buys.Commands;

namespace API.Controllers
{
    public class CollectionsController : BaseController
    {
        public CollectionsController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCollections([FromQuery] string Creator, [FromQuery] string Query, [FromQuery] double MinVolume = 0, [FromQuery] double MaxVolume = 1000.0d, [FromQuery] string SortBy = "date_added", [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllCollectionsQuery { Creator = Creator, Query = Query, MinVolume = MinVolume, MaxVolume = MaxVolume, SortBy = SortBy, PageNumber = PageNumber, PageSize = PageSize }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionsDto createCollectionDto)
        {
            return HandleResult(await Mediator.Send(new CreateCollectionCommand { CreateCollectionDto = createCollectionDto, UserAddress = _userAccessor.GetAddress() }));
        }

        [HttpGet("trending")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTrendingCollections([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetTrendingCollectionsQuery() { PageNumber = PageNumber, PageSize = PageSize }));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            return HandleResult(await Mediator.Send(new GetCollectionDetailsQuery { Id = id }));
        }
    }
}
