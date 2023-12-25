namespace Application.Features.Offers.Dtos
{
    public sealed record CreateOfferDto : IOfferDto
    {
        public double Amount  { get; set; }
        public string Asset { get; set; }
    }
}
