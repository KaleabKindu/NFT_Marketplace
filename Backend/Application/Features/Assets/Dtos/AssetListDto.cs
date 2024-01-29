using System;
using Application.Features.Common;

namespace Application.Features.Assets.Dtos
{
    public class AssetListDto : BaseDto
    {
        public string Category { get; set; }
        
        public string Name { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }

        public string Price { get; set; }
        
    }
}