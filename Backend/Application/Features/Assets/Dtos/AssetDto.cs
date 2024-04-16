namespace Application.Features.Assets.Dtos
{
    public class AssetDto
    {
        public string Name { get; set; }
        public long TokenId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public string Creator { get; set; }
        // public Category Category { get; set; }
        public string Price { get; set; }
        // public Auction Auction { get; set; }
        public long CollectionId { get; set; }
        public float Royalty { get; set; }
    }
}