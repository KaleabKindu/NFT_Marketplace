namespace Application.Features.Auth.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public double ExpireInDays { get; set; }
    }

    public class NonceDto
    {
        public string Nonce { get; set; }
    }
}