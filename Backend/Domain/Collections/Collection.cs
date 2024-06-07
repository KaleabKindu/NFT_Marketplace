
using Domain.Assets;

namespace Domain.Collections
{
    public class Collection : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Background { get; set; }
        public string Avatar { get; set; }
        public AppUser Creator { get; set; }
        public string CreatorId { get; set; }
        public double Volume { get; set; } = 0;
        public long Items { get; set; } = 0;
        public double FloorPrice { get; set; } = 0;
        public double LatestPrice { get; set; } = 0;
        public ICollection<Asset> Assets { get; set; }
    }
}