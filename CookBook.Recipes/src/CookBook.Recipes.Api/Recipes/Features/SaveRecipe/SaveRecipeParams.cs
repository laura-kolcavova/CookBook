using CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe;

internal sealed record SaveRecipeParams
{
    [FromBody]
    public required SaveRecipeRequestDto SaveRecipeRequest { get; init; }
}
