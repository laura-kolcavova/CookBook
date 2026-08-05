using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Validators;

internal sealed class SearchRecipesValidator :
    AbstractValidator<SearchRecipesParams>
{
    public SearchRecipesValidator()
    {
        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Limit)
            .GreaterThanOrEqualTo(0);
    }
}
