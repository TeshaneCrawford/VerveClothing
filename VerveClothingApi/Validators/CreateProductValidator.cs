﻿using FluentValidation;
using VerveClothingApi.DTOs;

namespace VerveClothingApi.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Product name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .LessThanOrEqualTo(10000)
                .WithMessage("Price must be between 0 and 10000");

            RuleFor(x => x.MaterialId)
                .GreaterThan(0)
                .WithMessage("A valid material must be selected");

            RuleFor(x => x.CategoryIds)
                .NotEmpty()
                .WithMessage("At least one category must be selected");
        }
    }
}
