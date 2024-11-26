using FluentValidation;
using VerveClothingApi.DTOs;

namespace VerveClothingApi.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .LessThan(10000);
        }
    }
}
