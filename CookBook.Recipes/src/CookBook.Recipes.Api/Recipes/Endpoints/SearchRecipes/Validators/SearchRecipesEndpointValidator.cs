using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Validators;

internal sealed class SearchRecipesEndpointValidator :
    AbstractValidator<SearchRecipesEndpointParams>
{
    public SearchRecipesEndpointValidator()
    {
        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Limit)
            .GreaterThanOrEqualTo(0);
    }
}
