using Application.Features.Common;
using Application.Profiles;

namespace Application.Features.Offers.Dtos
{
    public class OfferDto : BaseDto, IOfferDto
    {
        public UserProfile OfferProvider { get; set; }
        public double Amount  { get; set; }
        public string Asset { get; set; }
    }
}
