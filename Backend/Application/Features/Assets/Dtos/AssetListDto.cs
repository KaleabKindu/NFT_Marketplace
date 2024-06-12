using Application.Features.Auctions.Dtos;
using Application.Features.Common;
using Domain.Assets;

namespace Application.Features.Assets.Dtos
{
    public class AssetListDto : BaseDto
    {
        public int TokenId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Audio { get; set; }
        public AssetStatus Status { get; set; }
        public int Likes { get; set; }
        public bool Liked { get; set; } = false;
        public double Price { get; set; }
        public GetAuctionDto Auction { get; set; }
    }
}

