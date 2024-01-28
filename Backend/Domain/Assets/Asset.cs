using Domain.Bids;

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
        public string Category { get; set; }
        public string Price { get; set; }
        // public Auction Auction { get; set; }
        public long CollectionId { get; set; }
        public float Royalty { get; set; }
        public List<Bid> Bids { get; set; }
    }
}