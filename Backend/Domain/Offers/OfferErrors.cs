using ErrorOr;

namespace Domain.Offers
{

    public static class OfferError
    {
        public static Error NotFound => Error.NotFound("Offer.NotFound", "Offer not found");

        public static Error DuplicateCode => Error.Conflict("Activity.DuplicateCode", "Duplicate code");
    }
}