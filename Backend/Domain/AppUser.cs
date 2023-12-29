using Microsoft.AspNetCore.Identity;
namespace Domain
{
    public sealed class AppUser : IdentityUser
    {
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PublicAddress { get; set; }
        public string Nonce { get; set; }
    }
}