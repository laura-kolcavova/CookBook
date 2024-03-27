using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.RemoveCategory;

internal sealed class RemoveCategoryRequestValidator : AbstractValidator<RemoveCategoryRequestDto>
{
    public RemoveCategoryRequestValidator()
    {
        RuleFor(request => request.CategoryId)
            .NotNull()
            .GreaterThan(0);
    }
}
