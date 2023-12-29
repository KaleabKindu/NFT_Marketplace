using Microsoft.AspNetCore.Identity;
namespace Domain
{
    public sealed class AppUser : IdentityUser
    {
        public string Password { get; set; }
        public string FullName { get; set; }
    }

}