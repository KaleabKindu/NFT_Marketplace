using System;
using Application.Features.Auctions.Dtos;
using Application.Features.Auth.Dtos;
using Application.Features.Collections.Dtos;
using Domain.Assets;

namespace Application.Features.Assets.Dtos
{
    public class AssetDetailDto
    {

        public string Name { get; set; }
        public long TokenId { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Audio { get; set; }
        public string? Video { get; set; }
        public AssetCategory Category { get; set; }
        public string Price { get; set; }
        public UserFetchDto Creator { get; set; }
        public UserFetchDto Owner { get; set; }
        public GetAuctionDto?  Auction {get; set;}
        public float Royalty { get; set; }
        public string TransactionHash { get; set; }
        public CollectionDto? Collection { get; set; }
        public int Likes { get; set; }
        public bool Liked { get; set; }
    }
}