using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes;
using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail;
using CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;
using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe;
using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

namespace CookBook.Recipes.Api.Recipes;

internal static class CategoriesEndpoints
{
    public static RouteGroupBuilder AddRecipesEndpoints(this RouteGroupBuilder group)
    {
        var recipesGroup = group
            .MapGroup("/Recipes")
            .WithTags("Recipes");

        SaveRecipeEndpoint.Configure(recipesGroup);
        RemoveRecipeEndpoint.Configure(recipesGroup);
        SearchRecipesEndpoint.Configure(recipesGroup);
        GetLatestRecipesEndpoint.Configure(recipesGroup);
        GetRecipeDetailEndpoint.Configure(recipesGroup);

        return group;
    }
}
