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

        public string Bio { get; set; }

        public string Address { get; set; }

        public string Nonce { get; set; }
        public string ProfileBackgroundImage { get; set; }
        public int TotalSalesCount { get; set; }

        public List<string> Followers { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string Telegram { get; set; }
    }
}