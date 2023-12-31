using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Auth.Queries;
using Application.Features.Auth.Dtos;
using Application.Contracts;


namespace API.Controllers{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public AuthController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet("nonce")]
        public async Task<IActionResult> GetUserNonce([FromQuery] string PublicAddress)
        {
            return HandleResult(await Mediator.Send(new GetUserNonce(){ PublicAddress=PublicAddress }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] string PublicAddress)
        {
            return HandleResult(await Mediator.Send(new CreateUserCommand(){ PublicAddress=PublicAddress }));
        }

        [HttpPost("authenticate")]
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
    }
}
