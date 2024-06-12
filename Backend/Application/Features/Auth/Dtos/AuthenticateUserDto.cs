using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class AuthenticateDto
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string SignedNonce { get; set; }
    }
}