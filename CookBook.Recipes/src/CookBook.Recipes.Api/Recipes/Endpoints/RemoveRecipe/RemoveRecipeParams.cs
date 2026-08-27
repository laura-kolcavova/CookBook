using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;

internal sealed record RemoveRecipeParams
{
    [FromRoute]
    public required long RecipeId { get; init; }
}
