using System;

namespace Domain.Auctions
{
    public class Auction : BaseClass
    {
        public long TokenId { get; set; }

        public AppUser Seller { get; set; }
        public string FloorPrice { get; set; }
        public string HighestBid { get; set; }
        public AppUser HighestBidder { get; set; }
        public long AuctionEnd  {get; set;}
        
    }
}