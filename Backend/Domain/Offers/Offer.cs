namespace Domain.Offers
{
    public sealed class Offer:BaseClass{
        public AppUser OfferProvider { get; set; }
        public DateTime Date { get; set; }
        public string Asset { get; set; }
        public double Amount { get; set; }
    }
}