using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

internal sealed record SearchRecipesEndpointParams
{
    [FromQuery]
    public int? Offset { get; init; }

    [FromQuery]
    public int? Limit { get; init; }
}
