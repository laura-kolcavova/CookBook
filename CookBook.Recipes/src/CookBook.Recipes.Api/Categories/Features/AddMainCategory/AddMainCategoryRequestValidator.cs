using CookBook.Recipes.Application.Categories.ValidationRules;
using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.AddMainCategory;

internal sealed class AddMainCategoryRequestValidator : AbstractValidator<AddMainCategoryRequestDto>
{
    public AddMainCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .CategoryName();
    }
}
