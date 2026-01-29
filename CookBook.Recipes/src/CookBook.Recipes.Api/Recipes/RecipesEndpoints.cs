using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes;
using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail;
using CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;
using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe;
using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

namespace CookBook.Recipes.Api.Recipes;

internal static class RecipesEndpoints
{
    public static RouteGroupBuilder MapRecipesEndpoints(this RouteGroupBuilder group)
    {
        var recipesGroup = group
            .MapGroup("/recipes")
            .WithTags("Recipes");

        GetLatestRecipesEndpoint.Configure(recipesGroup);
        GetRecipeDetailEndpoint.Configure(recipesGroup);
        SearchRecipesEndpoint.Configure(recipesGroup);
        SaveRecipeEndpoint.Configure(recipesGroup);
        RemoveRecipeEndpoint.Configure(recipesGroup);

        return group;
    }
}
