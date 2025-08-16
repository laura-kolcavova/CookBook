using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.RemoveRecipe.Validators;

internal sealed class RemoveRecipeRequestValidator :
    AbstractValidator<RemoveRecipeParams>
{
    public RemoveRecipeRequestValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
