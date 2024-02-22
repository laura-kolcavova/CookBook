using CookBook.Recipes.Api.Features.Recipes.RemoveRecipe;
using CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

namespace CookBook.Recipes.Api.Features.Recipes;

internal static class RecipesEndpoints
{
    public static RouteGroupBuilder AddRecipesEndpoints(this RouteGroupBuilder group)
    {
        var recipesGroup = group
            .MapGroup("/recipes")
            .WithTags("Recipes");

        SaveRecipeEndpoint.Configure(recipesGroup);
        RemoveRecipeEndpoint.Configure(recipesGroup);

        return group;
    }
}
