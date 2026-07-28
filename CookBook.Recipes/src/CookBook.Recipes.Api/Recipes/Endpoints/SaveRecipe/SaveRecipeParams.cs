using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe;

internal sealed record SaveRecipeParams
{
    [FromBody]
    public required SaveRecipeRequestDto SaveRecipeRequest { get; init; }
}
