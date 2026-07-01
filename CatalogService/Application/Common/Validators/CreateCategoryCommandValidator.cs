using CatalogService.Application.Categories.Commands.CreateCategory;
using FluentValidation;

namespace CatalogService.Application.Common.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.ParentId)
                .NotNull()
                .WithMessage("ParentId is required");
        }
    }
}
