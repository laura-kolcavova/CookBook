using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe;

internal sealed record SaveRecipeEndpointParams
{
    [FromBody]
    public required SaveRecipeRequestDto SaveRecipeRequest { get; init; }
}
