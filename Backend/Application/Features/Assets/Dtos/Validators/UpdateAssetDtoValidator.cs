using System;
using FluentValidation;

namespace Application.Features.Assets.Dtos.Validators
{
    public class UpdateAssetDtoValidator : AbstractValidator<UpdateAssetDto>
    {
        public UpdateAssetDtoValidator()
        {
            
                RuleFor(dto => dto.Title).NotEmpty().WithMessage("Title is required.");
                RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required.");
                RuleFor(dto => dto.ImageUrl).NotEmpty().WithMessage("Image URL is required.");
                RuleFor(dto => dto.CategoryId).GreaterThan(0).WithMessage("Valid CategoryId is required.");
                RuleFor(dto => dto.CollectionId).GreaterThan(0).WithMessage("Valid CollectionId is required.");
                RuleFor(dto => dto.TotalSupply).GreaterThan(0).WithMessage("Valid TotalSupply is required.");
                RuleFor(dto => dto.WinningBidId).GreaterThan(0).WithMessage("Valid WinningBidId is required.");
                RuleFor(dto => dto.MetaData).NotEmpty().WithMessage("MetaData is required.");
                RuleFor(dto => dto.Royalties).GreaterThanOrEqualTo(0).WithMessage("Valid Royalties is required.");
            }

        }
    }

