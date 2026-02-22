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

        RuleFor(request => request.UserName)
          .NotNull()
          .Length(0, 256);
    }
}
