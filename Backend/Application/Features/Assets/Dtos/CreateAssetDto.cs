using System;
using Application.Features.Categories.Dtos;
using Domain;
using Domain.Categories;

namespace Application.Features.Assets.Dtos
{
    public class CreateAssetDto
    {
        public string Name { get; set; }
        public long TokenId {get; set;}
        public string Description { get; set; }
        public string Image { get; set; }
    
        public string Category { get; set; }
        public string Price { get; set; }
        // public Auction Auction { get; set; }
        public long CollectionId { get; set; }
        public string TransactionHash { get; set; }
        public float Royalty { get; set; }


    }
}