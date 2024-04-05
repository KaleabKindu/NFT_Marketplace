using System;
using Application.Features.Auctions.Dtos;
using Domain.Assets;

namespace Application.Features.Assets.Dtos
{
    public class CreateAssetDto
    {
        public string Name { get; set; }
        public long TokenId {get; set;}
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Audio { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public CreateAuctionDto Auction { get; set; }
        public long? CollectionId { get; set; }
        public string TransactionHash { get; set; }
        public float Royalty { get; set; }
        public AssetStatus Status { get; set; } = AssetStatus.OnSale;
    }
}