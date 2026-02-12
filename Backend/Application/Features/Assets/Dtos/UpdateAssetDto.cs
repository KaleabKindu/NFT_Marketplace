using System;
using Application.Features.Common;
using Domain;

namespace Application.Features.Assets.Dtos
{
    public class UpdateAssetDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public long CollectionId { get; set; }
        public int TotalSupply { get; set; }
        public long WinningBidId { get; set; }
        public string MetaData { get; set; }
        public float Royalties { get; set; }
    }
}