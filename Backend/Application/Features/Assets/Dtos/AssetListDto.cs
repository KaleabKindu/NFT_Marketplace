using System;

namespace Application.Features.Assets.Dtos
{
    public class AssetListDto
    {
        public string Category { get; set; }
        
        public string Name { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }

        public string Price { get; set; }
        
    }
}