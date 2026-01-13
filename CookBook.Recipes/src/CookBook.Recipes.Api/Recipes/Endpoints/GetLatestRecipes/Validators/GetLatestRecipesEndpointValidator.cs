using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Validators;

internal sealed class GetLatestRecipesEndpointValidator :
    AbstractValidator<GetLatestRecipesEndpointParams>
{
    public GetLatestRecipesEndpointValidator()
    {
        RuleFor(request => request.Count)
             .GreaterThanOrEqualTo(0);
    }
}
