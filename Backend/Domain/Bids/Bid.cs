using Domain.Assets;

namespace Domain.Bids
{
    public class Bid: BaseClass
    {
        public AppUser Bidder { set; get; }
        public string BidderId { set; get; }
        public Asset Asset { set; get; }
        public long AssetId { set; get; }
        public double Amount  { set; get; }
        public string TransactionHash { set; get; }
    }
}