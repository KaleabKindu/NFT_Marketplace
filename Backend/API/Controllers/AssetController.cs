using Application.Features.Assets.Command;
using Application.Features.Assets.Query;
using Application.Features.Assets.Dtos;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{

    
    public class AssetsController : BaseController
    {
        public AssetsController(IUserAccessor userAccessor):base(userAccessor){}

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            return HandleResult(await Mediator.Send(new GetAssetByIdQuery { Id = id }));
        }
        

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetAllAssetQuery { PageNumber = pageNumber, PageSize = pageSize }));
        }
        

        [AllowAnonymous]
        [HttpGet("open-auction")]
        public async Task<IActionResult> Get ([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10){
           
            return HandleResult(await Mediator.Send(new GetAssetsWOpenAuctQuery { PageNumber = pageNumber, PageSize = pageSize }));


        }

        [HttpPost("mint")]

        public async Task<IActionResult> Post([FromBody] CreateAssetDto createAssetDto)
        {

            return HandleResult( await Mediator.Send(new CreateAssetCommand { CreateAssetDto = createAssetDto , PublicAddress = _userAccessor.GetPublicAddress() }));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAssetDto updateAssetDto)
        {

            return HandleResult(await Mediator.Send(new UpdateAssetCommand { UpdateAssetDto = updateAssetDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return HandleResult(await Mediator.Send(new DeleteAssetCommand { Id = id }));
        }

    }
}