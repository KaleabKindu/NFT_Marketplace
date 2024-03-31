using Application.Contracts;
using Application.Features.Busy.Dtos;
using Application.Features.Buys.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuyController : BaseController
    {
        public BuyController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }    

        [HttpPost]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> CreateBuy([FromBody] BuyAssetDto BuyAsset)
        {
            return  HandleResult(await Mediator.Send(new BuyAssetCommand { BuyAsset = BuyAsset, UserAddress =  _userAccessor.GetAddress()}));
        }
    }
}