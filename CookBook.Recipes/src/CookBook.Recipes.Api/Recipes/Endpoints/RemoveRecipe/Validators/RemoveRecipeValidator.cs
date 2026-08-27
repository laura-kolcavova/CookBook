using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe.Validators;

internal sealed class RemoveRecipeValidator :
    AbstractValidator<RemoveRecipeParams>
{
    public RemoveRecipeValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
