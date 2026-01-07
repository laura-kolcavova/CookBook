using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail;

internal sealed record GetRecipeDetailEndpointParams
{
    [FromRoute]
    public required long RecipeId { get; init; }
}
