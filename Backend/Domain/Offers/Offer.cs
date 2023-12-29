namespace Domain
{
    public sealed class Offer: BaseClass{
        public AppUser OfferProvider { get; set; }
        public string Asset { get; set; }
        public double Amount { get; set; }
    }
}