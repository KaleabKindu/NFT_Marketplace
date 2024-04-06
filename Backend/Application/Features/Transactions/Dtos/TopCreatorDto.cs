using Nethereum.Web3.Accounts;

namespace Application.Features.Transactions.Dtos
{
    public class TopCreatorDto
    {
        public string username { get; set; }
        public string background { get; set; }

        public string avatar { get; set; }

        public string address { get; set; }

        public string sales { get; set; }

        public bool following { get; set; }
    }
}