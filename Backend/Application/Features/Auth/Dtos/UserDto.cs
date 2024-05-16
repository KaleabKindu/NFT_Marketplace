using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Nonce { get; set; }
    }
}