using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

internal sealed record SearchRecipesParams
{
    [FromQuery]
    public string? SearchTerm { get; init; }

    [FromQuery]
    public int? Offset { get; init; }

    [FromQuery]
    public int? Limit { get; init; }
}
