using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Features.SearchRecipes;

internal sealed record SearchRecipesParams
{
    [FromQuery]
    public int? Offset { get; init; }

    [FromQuery]
    public int? Limit { get; init; }
}
