using System;
using Application.Features.Auctions.Dtos;
using Application.Features.Common;

namespace Application.Features.Assets.Dtos
{
    public class AssetListDto : BaseDto
    {
        public int TokenId { get; set; }
        public string Category { get; set; }
        
        public string Name { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }

        public double Price { get; set; }
        public GetAuctionDto Auction { get; set; }
        
    }
}

