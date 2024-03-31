namespace Application.Features.Auth.Dtos
{
    public class AuthenticateDto
    {
        public string Address { get; set; }
        public string SignedNonce { get; set; }
    }
}