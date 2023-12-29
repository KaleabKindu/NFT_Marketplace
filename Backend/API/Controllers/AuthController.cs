using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Auth.Queries;


namespace API.Controllers{

    public class AuthController : BaseController
    {
        [HttpGet("/nonce")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserNonce([FromBody] string PublicAddress)
        {
            return HandleResult(await Mediator.Send(new GetUserNonce(){ PublicAddress=PublicAddress }));
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] string PublicAddress)
        {
            return HandleResult(await Mediator.Send(new CreateUserCommand(){ PublicAddress=PublicAddress }));
        }

        [HttpPost("/authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] string publicAddress, [FromBody] string signedNonce)
        {
            return HandleResult(await Mediator.Send(new AuthenticateUserCommand(){ PublicAddress=publicAddress, SignedNonce=signedNonce }));
        }
    }
}
