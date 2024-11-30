
using FluentValidation;
using VerveClothingApi.DTOs;

namespace VerveClothingApi.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.Name != null)
                .WithMessage("Product name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000)
                .When(x => x.Description != null)
                .WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .LessThanOrEqualTo(10000)
                .When(x => x.BasePrice.HasValue)
                .WithMessage("Price must be between 0 and 10000");

            RuleFor(x => x.MaterialId)
                .GreaterThan(0)
                .When(x => x.MaterialId.HasValue)
                .WithMessage("A valid material must be selected");
        }
    }
}