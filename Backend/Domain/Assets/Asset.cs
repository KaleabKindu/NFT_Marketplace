using System.Text.Json.Serialization;
using Domain.Auctions;
using Domain.Bids;
using Domain.Collections;

namespace Domain.Assets
{
    public class Asset : BaseClass
    {
        public string Name { get; set; }
        public long TokenId {get; set;}
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Audio { get; set; }
        public AppUser Owner { get; set; }
        public string OwnerId { get; set; }
        public AppUser Creator { get; set; }
        public string CreatorId { get; set; }
        public AssetCategory Category { get; set; }
        public double Price { get; set; }
        public Auction? Auction { get; set; }
        public long? AuctionId { get; set; }
        public Collection? Collection { get; set; }
        public long? CollectionId { get; set; }
        public float Royalty { get; set; }
        public List<Bid> Bids { get; set; }
        public int Likes { get; set; }
        public string TransactionHash { get; set; }
        public AssetStatus Status { get; set; }
    }

    public enum  AssetCategory
    {
      
      audio, 
      video, 
      art, 
      photography, 
      ticket, 
      design, 
      ebook, 
      three_d
    }
    
    public enum AssetStatus {
        OnSale,
        Sold,
        Auction,
        NotForSale
    }
}