using Domain;

namespace Application.Features.Offers.Dtos
{
    public interface IOfferDto
    {
        public double Amount  { get; set; }
        public string Asset { get; set; }
    }
}
