using Application.Features.Auth.Dtos;
using Application.Features.Common;

namespace Application.Features.Collections.Dtos
{
    public class CreateCollectionsDto
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}