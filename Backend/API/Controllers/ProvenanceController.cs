using Application.Contracts;
using Application.Features.Provenances.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[AllowAnonymous]
public class ProvenanceController : BaseController
{
    public ProvenanceController(IUserAccessor userAccessor) : base(userAccessor)
    {
    }

    [HttpGet("{assetId}")]
    public async Task<IActionResult> Get([FromRoute] long assetId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)

    {

        return HandleResult(await Mediator.Send(new GetProvenanceQuery
        { AssetId = assetId, PageNumber = pageNumber, PageSize = pageSize }));

    }

}