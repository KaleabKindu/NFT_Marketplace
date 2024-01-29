using System;
using Application.Features.Auctions.Dtos;
using Application.Features.Auth.Dtos;

namespace Application.Features.Assets.Dtos
{
    public class AssetDetailDto
    {

        public string Name { get; set; }
        public long TokenId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public UserFetchDto Creator { get; set; }
        public UserFetchDto Owner { get; set; }

        public GetAuctionDto   Auction {get; set;}
        public float Royalty { get; set; }
        public int likes {get; set;}


        
    }
}