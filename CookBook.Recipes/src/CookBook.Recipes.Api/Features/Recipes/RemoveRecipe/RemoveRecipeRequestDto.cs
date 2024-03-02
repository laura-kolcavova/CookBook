using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Features.Recipes.RemoveRecipe;

internal sealed record RemoveRecipeRequestDto
{
    [FromRoute]
    public required long RecipeId { get; init; }
}
