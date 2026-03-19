using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail;

internal sealed record GetRecipeDetailEndpointParams
{
    [FromRoute]
    public required long RecipeId { get; init; }
}
