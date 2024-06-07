using Microsoft.AspNetCore.Mvc;

using Application.Contracts;
using Application.Features.UserProfiles.Dtos;
using Application.Features.UserProfiles.Commands;


namespace API.Controllers
{
    public class UserProfileController : BaseController
    {

        public UserProfileController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new UpdateProfileCommand
                    {
                        UpdateProfileDto = updateProfileDto,
                        Address = _userAccessor.GetAddress()
                    }
                )
            );

        }
    }
}