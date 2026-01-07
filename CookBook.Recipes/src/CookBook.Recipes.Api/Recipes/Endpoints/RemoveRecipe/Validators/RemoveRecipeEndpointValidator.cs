using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe.Validators;

internal sealed class RemoveRecipeEndpointValidator :
    AbstractValidator<RemoveRecipeEndpointParams>
{
    public RemoveRecipeEndpointValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
