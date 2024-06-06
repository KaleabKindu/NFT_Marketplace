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

    [HttpGet("{tokenId}")]
    public async Task<IActionResult> Get([FromRoute] long tokenId, int pageNumber = 1, int pageSize = 10)

    {

        return HandleResult(await Mediator.Send(new GetProvenanceQuery
        { TokenId = tokenId, PageNumber = pageNumber, PageSize = pageSize }));

    }

}