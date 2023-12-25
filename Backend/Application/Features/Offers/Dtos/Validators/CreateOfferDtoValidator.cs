using FluentValidation;

namespace Application.Features.Offers.Dtos.Validators
{
    public class CreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
    {
        public CreateOfferDtoValidator()
        {
            Include(new OfferDtoValidator());
        }
    }
}
