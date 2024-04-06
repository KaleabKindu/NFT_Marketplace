
namespace Domain.Collections
{
    public class Collection : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }
        public string Avatar { get; set; }
        public string Category { get; set; }
        public AppUser Creator { get; set; }
        public double Volume { get; set; }
        public long Items { get; set; }
        public double FloorPrice { get; set; }
        public double LatestPrice { get; set; }
    }
}