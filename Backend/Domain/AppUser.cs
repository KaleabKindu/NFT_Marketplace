#nullable enable
using Microsoft.AspNetCore.Identity;
namespace Domain
{
    public sealed class AppUser : IdentityUser
    {
        override
        public string? UserName { get; set; }

        override
        public string? Email { get; set; }

        public string? Avatar { get; set; } = "";

        public string PublicAddress { get; set; }

        public string Nonce { get; set; }
    }
}