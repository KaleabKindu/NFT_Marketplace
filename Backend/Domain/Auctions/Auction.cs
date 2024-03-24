using System;

namespace Domain.Auctions
{
    public class Auction : BaseClass
    {
        public long AuctionId {get; set;}
        public long TokenId { get; set; }
        public AppUser Seller { get; set; }
        public double FloorPrice { get; set; }
        public double HighestBid { get; set; }
        public AppUser HighestBidder { get; set; }
        public long AuctionEnd  {get; set;}
        
    }
}