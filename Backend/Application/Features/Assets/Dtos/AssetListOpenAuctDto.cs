using System;

namespace Application.Features.Assets.Dtos
{
    public class AssetListOpenAuctDto : AssetListDto
    {
        public long AuctionEnd { get; set; }
        
    }
}