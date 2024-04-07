using Application.Features.Assets.Command;
using Application.Features.Assets.Query;
using Application.Features.Assets.Dtos;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Domain.Assets;
using Application.Features.Categories.Queries;


namespace API.Controllers
{

    
    public class AssetsController : BaseController
    {
        public AssetsController(IUserAccessor userAccessor):base(userAccessor){}

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId =  _userAccessor.GetUserId();
            return HandleResult(await Mediator.Send(new GetAssetByIdQuery { Id = id, UserId = userId }));
        }
        

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string query,
            [FromQuery] double min_price, 
            [FromQuery] double max_price, 
            [FromQuery] AssetCategory? category, 
            [FromQuery] string sort_by, 
            [FromQuery] string sale_type, 
            [FromQuery] long? collectionId, 
            [FromQuery] string creatorId, 
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10
        )
        {
            var userId =  _userAccessor.GetUserId();

            return HandleResult(await Mediator.Send(new GetAllAssetQuery { 
                    UserId = userId,
                    Query = query,
                    MinPrice = min_price,
                    MaxPrice = max_price,
                    Category = category,
                    SortBy = sort_by,
                    SaleType = sale_type,
                    CollectionId = collectionId,
                    CreatorId = creatorId,
                    PageNumber = pageNumber, 
                    PageSize = pageSize 
                }));
        }
        

        [AllowAnonymous]
        [HttpGet("open-auction")]
        public async Task<IActionResult> Get ([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10){
           
            return HandleResult(await Mediator.Send(new GetAssetsWOpenAuctQuery { PageNumber = pageNumber, PageSize = pageSize }));


        }

        [HttpPost("mint")]
        public async Task<IActionResult> Post([FromBody] CreateAssetDto createAssetDto)
        {

            return HandleResult( await Mediator.Send(new CreateAssetCommand { CreateAssetDto = createAssetDto , Address = _userAccessor.GetAddress() }));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAssetDto updateAssetDto)
        {

            return HandleResult(await Mediator.Send(new UpdateAssetCommand { UpdateAssetDto = updateAssetDto }));
        }

        [HttpPut("toggle-like/{id}")]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var userId =  _userAccessor.GetUserId();

            if (userId == null)
                return Unauthorized();

            return HandleResult(await Mediator.Send(new ToggleLikeAssetCommand { Id = id, UserId = userId}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return HandleResult(await Mediator.Send(new DeleteAssetCommand { Id = id }));
        }


        [HttpGet("assets/{categoryName}")]
        public async Task<IActionResult> GetAssetsCount([FromQuery] AssetCategory categoryName)
        {
            return HandleResult(await Mediator.Send(new GetAssetCountByCategoryQuery { CategoryName = categoryName }));
        }

    }
}