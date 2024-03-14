using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.RemoveRecipe;

internal sealed class RemoveRecipeRequestValidator : AbstractValidator<RemoveRecipeRequestDto>
{
    public RemoveRecipeRequestValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
