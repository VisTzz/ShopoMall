using CatalogService.Application.Categories.Commands.CreateCategory;
using FluentValidation;

namespace CatalogService.Application.Common.Validators
{
    public class CreateCategoryCommonValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommonValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Name cannot exeed 100 charachters");
        }
    }
}