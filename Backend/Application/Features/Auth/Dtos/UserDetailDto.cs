namespace Application.Features.Auth.Dtos
{
    public class UserDetailDto
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }

        public string EthereumAddress { get; set; }
        public string ProfileBackgroundImage { get; set; }
        public int TotalSalesCount { get; set; }

        public List<string> Followers { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string Telegram { get; set; }

    }
}
