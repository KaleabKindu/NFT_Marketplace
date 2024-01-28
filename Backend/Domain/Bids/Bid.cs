using Domain.Assets;

namespace Domain.Bids
{
    public class Bid: BaseClass
    {
        public AppUser Bidder { set; get; }

        public Asset Asset { set; get; }

        public double Amount  { set; get; }

        public string TransactionHash { set; get; }
    }
}