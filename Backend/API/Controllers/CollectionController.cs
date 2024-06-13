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
        public async Task<IActionResult> GetAllCollections([FromQuery] string creator, [FromQuery] string query, [FromQuery] double minVolume = 0, [FromQuery] double maxVolume = 1000.0d, [FromQuery] string sortBy = "date_added", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllCollectionsQuery { Creator = creator, Query = query, MinVolume = minVolume, MaxVolume = maxVolume, SortBy = sortBy, PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionsDto createCollectionDto)
        {
            return HandleResult(await Mediator.Send(new CreateCollectionCommand { CreateCollectionDto = createCollectionDto, UserAddress = _userAccessor.GetAddress() }));
        }

        [HttpGet("trending")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTrendingCollections([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetTrendingCollectionsQuery() { PageNumber = pageNumber, PageSize = pageSize }));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            return HandleResult(await Mediator.Send(new GetCollectionDetailsQuery { Id = id }));
        }
    }
}
