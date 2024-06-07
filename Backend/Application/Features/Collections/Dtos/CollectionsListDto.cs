using Application.Features.Auth.Dtos;
using Application.Features.Common;

namespace Application.Features.Collections.Dtos
{
    public class CollectionsListDto : BaseDto
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public UserFetchDto Creator { get; set; }
        public string Volume { get; set; }
        public string FloorPrice { get; set; }
        public long Items { get; set; }
    }
}