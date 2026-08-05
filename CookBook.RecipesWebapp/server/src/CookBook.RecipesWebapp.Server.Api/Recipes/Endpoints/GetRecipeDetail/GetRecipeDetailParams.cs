using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail;

internal sealed record GetRecipeDetailParams
{
    [FromRoute]
    public required long RecipeId { get; init; }
}
