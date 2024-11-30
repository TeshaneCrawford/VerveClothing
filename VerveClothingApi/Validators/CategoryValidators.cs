
using FluentValidation;
using VerveClothingApi.DTOs;
using VerveClothingApi.Interfaces;

namespace VerveClothingApi.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters");

            RuleFor(x => x.ParentCategoryId)
                .MustAsync(async (id, cancellation) =>
                {
                    if (!id.HasValue) return true;
                    return await categoryRepository.ExistsAsync(id.Value);
                })
                .WithMessage("Specified parent category does not exist");
        }
    }

    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Category name must not exceed 100 characters");

            RuleFor(x => x.ParentCategoryId)
                .MustAsync(async (id, cancellation) =>
                {
                    if (!id.HasValue) return true;
                    return await categoryRepository.ExistsAsync(id.Value);
                })
                .WithMessage("Specified parent category does not exist");
        }
    }
}