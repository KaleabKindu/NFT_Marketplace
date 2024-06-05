using System;
using FluentValidation;

namespace Application.Features.Assets.Dtos.Validators
{
    public class CreateAssetDtoValidator : AbstractValidator<CreateAssetDto>
{
    public CreateAssetDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(dto => dto.TokenId).GreaterThan(0).WithMessage("Valid TokenId is required.");
        RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(dto => dto.Image).NotEmpty().WithMessage("Image Or Video is required.").When(x => string.IsNullOrEmpty(x.Video));
        RuleFor(dto => dto.Video).NotEmpty().WithMessage("Video Or Image is required.").When(x => string.IsNullOrEmpty(x.Image));
        // Update the validation rules according to your requirements for the new properties
        RuleFor(dto => dto.Category).NotNull().WithMessage("Category is required.");
        RuleFor(dto => dto.Price).NotEmpty().WithMessage("Price is required.");
        // RuleFor(dto => dto.Auction).NotNull().WithMessage("Auction is required.");
        RuleFor(dto => dto.Royalty).GreaterThanOrEqualTo(0).WithMessage("Valid Royalty is required.");
    }
}

}