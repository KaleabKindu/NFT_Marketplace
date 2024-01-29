using System;

namespace Application.Features.Auctions.Dtos
{
    public class GetAuctionDto 
    {
        public long AuctionId { get; set; }
        public long AuctionEnd { get; set; }
        public string CurrentPrice {get; set;}
        
    }
}