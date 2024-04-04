using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.GetCategories;

internal sealed class GetCategoriesRequestValidator : AbstractValidator<GetCategoriesRequestDto>
{
    public GetCategoriesRequestValidator()
    {
        RuleFor(request => request.ParentCategoryId)
            .GreaterThan(0);
    }
}
