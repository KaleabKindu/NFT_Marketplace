using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Auth.Dtos;
using Application.Contracts;
using Application.Features.Auth.Queries;


namespace API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public AuthController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpPost("users/create-fetch")]
        public async Task<IActionResult> CreateOrFetch([FromQuery] string Address)
        {
            return HandleResult(
                await Mediator.Send(
                    new CreateOrFetchUserCommand(){ 
                        Address=Address 
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
                        Address=authenticateDto.Address, 
                        SignedNonce=authenticateDto.SignedNonce
                    }
                )
            );
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, int pageSize = 10)
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

    [HttpGet("user/detail")]
    public async Task<IActionResult> GetUserDetails([FromQuery] string publicAddress)
    {
        return HandleResult(
            await Mediator.Send(
                new GetUserDetailQuery
                {
                    publicAddress = publicAddress
                }
            )
        );
    }
    }
}