using System;

namespace Application.Features.Auctions.Dtos
{
    public class CreateAuctionDto
    {
        public long AuctionId { get; set; }
        public long AuctionEnd { get; set; }
        
    }
}