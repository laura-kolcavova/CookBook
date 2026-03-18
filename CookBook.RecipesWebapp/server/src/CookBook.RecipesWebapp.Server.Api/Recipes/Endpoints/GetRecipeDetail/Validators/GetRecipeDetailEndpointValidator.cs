using FluentValidation;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Validators;

internal sealed class GetRecipeDetailEndpointValidator :
    AbstractValidator<GetRecipeDetailEndpointParams>
{
    public GetRecipeDetailEndpointValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
