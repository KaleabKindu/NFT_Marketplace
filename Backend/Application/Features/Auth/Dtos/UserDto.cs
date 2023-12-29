using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class UserDto: BaseDto
    {
        public string PublicAddress { get; set; }
        public string Nonce { get; set; }
    }
}