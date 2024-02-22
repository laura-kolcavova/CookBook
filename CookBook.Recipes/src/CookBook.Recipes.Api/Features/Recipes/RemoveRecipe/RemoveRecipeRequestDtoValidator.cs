using FluentValidation;

namespace CookBook.Recipes.Api.Features.Recipes.RemoveRecipe;

internal class RemoveRecipeRequestDtoValidator : AbstractValidator<RemoveRecipeRequestDto>
{
    public RemoveRecipeRequestDtoValidator()
    {
        RuleFor(request => request.RecipeId)
            .GreaterThanOrEqualTo(0);
    }
}
