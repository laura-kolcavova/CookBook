using CookBook.Recipes.Application.Categories.ValidationRules;
using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.AddSubCategory;

internal sealed class AddSubCategoryRequestValidator : AbstractValidator<AddSubCategoryRequestDto>
{
    public AddSubCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .CategoryName();

        RuleFor(request => request.ParentCategoryId)
            .NotNull()
            .GreaterThan(0);
    }
}
