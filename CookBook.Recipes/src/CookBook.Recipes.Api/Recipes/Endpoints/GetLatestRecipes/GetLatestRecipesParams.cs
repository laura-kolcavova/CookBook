using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes;

internal sealed record GetLatestRecipesParams
{
    [FromQuery]
    public required int Count { get; init; }
}
