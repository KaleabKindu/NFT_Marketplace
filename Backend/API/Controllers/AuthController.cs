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
                    new CreateOrFetchUserCommand()
                    {
                        Address = Address
                    }
                )
            );
        }

        [HttpPost("users/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto authenticateDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new AuthenticateUserCommand()
                    {
                        Address = authenticateDto.Address,
                        SignedNonce = authenticateDto.SignedNonce
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
        public async Task<IActionResult> GetUserDetails([FromQuery] string address)
        {
            return HandleResult(
                await Mediator.Send(
                    new GetUserDetailQuery
                    {
                        address = address
                    }
                )
            );
        }

        [HttpGet("users/network/{address}")]
        public async Task<IActionResult> GetNetwork([FromRoute] string address, [FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return HandleResult(
                await Mediator.Send(
                    new GetUserNetwork
                    {
                        CurrentUserAddress = _userAccessor.GetAddress(),
                        Address = address,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Type = type
                    }
                )
            );
        }
    }
}