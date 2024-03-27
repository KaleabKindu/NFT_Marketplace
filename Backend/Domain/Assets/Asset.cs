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
        public string Image { get; set; }
        public AppUser Owner { get; set; }
        public AppUser Creator { get; set; }
        public AssetCategory Category { get; set; }
        public double Price { get; set; }
        public Auction Auction { get; set; }
        public Collection? Collection { get; set; }
        public long? CollectionId { get; set; }
        public float Royalty { get; set; }
        public List<Bid> Bids { get; set; }
    }

    public enum  AssetCategory
    {
       image,
       audio,
       video,
       art,
       ticket,
       design,
       drawing
    }
}