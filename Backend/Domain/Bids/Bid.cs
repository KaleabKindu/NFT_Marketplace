namespace Domain.Bids
{
    public class Bid: BaseClass
    {
        public string Bidder { set; get; }

        public string Asset { set; get; }

        public double Amount  { set; get; }
    }
}