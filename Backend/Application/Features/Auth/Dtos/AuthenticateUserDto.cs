namespace Application.Features.Auth.Dtos
{
    public class AuthenticateDto
    {
        public string PublicAddress { get; set; }
        public string SignedNonce { get; set; }
    }
}