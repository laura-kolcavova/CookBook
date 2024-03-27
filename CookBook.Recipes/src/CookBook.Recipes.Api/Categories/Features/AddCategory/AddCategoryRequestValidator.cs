using CookBook.Recipes.Application.Categories.ValidationRules;
using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.AddCategory;

internal sealed class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequestDto>
{
    public AddCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .CategoryName();

        RuleFor(request => request.ParentCategoryId)
            .GreaterThan(0);
    }
}
