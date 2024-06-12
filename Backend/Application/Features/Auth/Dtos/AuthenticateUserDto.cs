using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class AuthenticateDto : BaseDto
    {
        public string Address { get; set; }
        public string SignedNonce { get; set; }
    }
}