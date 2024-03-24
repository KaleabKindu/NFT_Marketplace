using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Auth.Dtos;
using Application.Contracts;
using Application.Features.Auth.Queries;


namespace API.Controllers{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public AuthController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpPost("users/create-fetch")]
        public async Task<IActionResult> CreateOrFetch([FromQuery] string PublicAddress)
        {
            return HandleResult(
                await Mediator.Send(
                    new CreateOrFetchUserCommand(){ 
                        PublicAddress=PublicAddress 
                    }
                )
            );
        }

        [HttpPost("users/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto authenticateDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new AuthenticateUserCommand(){ 
                        PublicAddress=authenticateDto.PublicAddress, 
                        SignedNonce=authenticateDto.SignedNonce
                    }
                )
            );
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, int pageSize)
        {
            return HandleResult(
                await Mediator.Send(
                    new GetUsersQuery
                    {
                        PageSize = pageSize,
                        PageNumber = pageNumber
                    }
        )
        );
    }
    }
}
