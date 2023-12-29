using FluentValidation;


namespace Application.Features.Offers.Dtos.Validators
{
    public class OfferDtoValidator : AbstractValidator<IOfferDto>
    {
        public OfferDtoValidator()
        {
            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo( 0).WithMessage("{PropertyName} must be greater or equal to {comparision value}");

            RuleFor(p => p.Asset)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
