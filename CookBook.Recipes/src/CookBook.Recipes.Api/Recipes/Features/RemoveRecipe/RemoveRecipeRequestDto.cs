using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Features.RemoveRecipe;

internal sealed class RemoveRecipeRequestDto
{
    [FromRoute]
    public long RecipeId { get; init; }
}
