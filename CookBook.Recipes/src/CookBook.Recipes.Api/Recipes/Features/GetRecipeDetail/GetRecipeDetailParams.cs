using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;

internal sealed record GetRecipeDetailParams
{
    [FromRoute]
    public required long RecipeId { get; init; }
}
