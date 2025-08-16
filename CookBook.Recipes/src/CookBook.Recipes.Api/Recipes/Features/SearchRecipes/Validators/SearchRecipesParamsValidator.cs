using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.SearchRecipes.Validators;

internal sealed class SearchRecipesParamsValidator :
    AbstractValidator<SearchRecipesParams>
{
    public SearchRecipesParamsValidator()
    {
        RuleFor(request => request.Offset)
             .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Limit)
            .GreaterThanOrEqualTo(0);
    }
}
