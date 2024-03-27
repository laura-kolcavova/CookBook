using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.RenameCategory;

internal sealed class RenameCategoryRequestValidator : AbstractValidator<RenameCategoryRequestDto>
{
    public RenameCategoryRequestValidator()
    {
        RuleFor(request => request.CategoryId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(request => request.Name)
            .NotEmpty();
    }
}
