using Microsoft.AspNetCore.Identity;
namespace Domain
{
    public sealed class AppUser : IdentityUser
    {
        public string Address { get; set; }
        public string Nonce { get; set; }
        public UserProfile Profile { get; set; }
        public long ProfileId { get; set; }
    }

    public class UserProfile : BaseClass
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Avatar { get; set; } = "";

        public string Bio { get; set; } = "";

        public string ProfileBackgroundImage { get; set; } = "";

        public int TotalSalesCount { get; set; } = 0;
        public double Volume { get; set; } = 0.0;
        public List<AppUser> Followers { get; set; } = new();
        public string Facebook { get; set; } = "";
        public string Twitter { get; set; } = "";
        public string YouTube { get; set; } = "";
        public string Telegram { get; set; } = "";
    }
}