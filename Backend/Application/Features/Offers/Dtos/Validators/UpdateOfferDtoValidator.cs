using FluentValidation;

namespace Application.Features.Offers.Dtos.Validators
{
    public class UpdateOfferDtoValidator : AbstractValidator<UpdateOfferDto>
    {
        public UpdateOfferDtoValidator()
        {

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo( 0).WithMessage("{PropertyName} must be greater or equal to {comparision value}");
        }
    }
}
