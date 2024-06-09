namespace Application.Features.Auth.Dtos
{
    public class UserNetworkDto
    {
        public string UserName { get; set; }
        public string Background { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Sales { get; set; }
        public Boolean Following { get; set; } = false;
    }
}