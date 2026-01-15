using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes;

internal sealed record GetLatestRecipesEndpointParams
{
    [FromQuery]
    public required int Count { get; init; }
}
