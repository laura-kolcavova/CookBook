using FluentValidation;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Validators;

internal sealed class GetRecipeDetailEndpointValidator :
    AbstractValidator<GetRecipeDetailParams>
{
    public GetRecipeDetailEndpointValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
