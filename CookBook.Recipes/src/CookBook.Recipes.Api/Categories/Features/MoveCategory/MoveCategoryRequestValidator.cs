using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.MoveCategory;

internal sealed class MoveCategoryRequestValidator : AbstractValidator<MoveCategoryRequestDto>
{
    public MoveCategoryRequestValidator()
    {
        RuleFor(request => request.CategoryId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(request => request.NewParentCategoryId)
            .NotNull()
            .GreaterThan(0);
    }
}
