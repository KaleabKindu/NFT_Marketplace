using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string PublicAddress { get; set; }
        public string Nonce { get; set; }
    }
}