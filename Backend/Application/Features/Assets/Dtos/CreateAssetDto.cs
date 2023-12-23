using System;
using Domain;

namespace Application.Features.Assets.Dtos
{
    public class CreateAssetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string CreatorRoyality { get; set; }
        public string Type { get; set; }
        public string ContractAddress { get; set; }
        public Guid TokenId { get; set; }

        public AppUser owner { get; set; }

        
    }
}