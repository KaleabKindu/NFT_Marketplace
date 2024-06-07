using Application.Features.Common;

namespace Application.Features.Collections.Dtos
{
    public class CollectionDetailsDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }
        public string Avatar { get; set; }
        public string FloorPrice { get; set; }
        public string Volume { get; set; }
        public long Items { get; set; }
        public string LatestPrice { get; set; }
    }
}