using FluentValidation;

namespace CookBook.Recipes.Api.Features.Recipes.RemoveRecipe;

internal class RemoveRecipeRequestValidator : AbstractValidator<RemoveRecipeRequestDto>
{
    public RemoveRecipeRequestValidator()
    {
        RuleFor(request => request.RecipeId)
            .GreaterThanOrEqualTo(0);
    }
}
