using Application.Contracts;
using Application.Features.Transactions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionController : BaseController
    {

        public TransactionController(IUserAccessor userAccessor) : base(userAccessor)
        {

        }

        [HttpGet]
        [Authorize(Roles = "Admin, Trader")]
        public async Task<IActionResult> GetTransactions([FromQuery] int assetId,  [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) 
        {
            return HandleResult(await Mediator.Send(new GetAllTransactionsQuery() { PageNumber = pageNumber, PageSize = pageSize, AssetId = assetId }));
        }

        [HttpGet("users/top-creators")]
        public async Task<IActionResult> GetTopCreators([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(await Mediator.Send(new GetTopCreatorsQuery() { PageNumber = pageNumber, PageSize = pageSize }));
        }
    }
}