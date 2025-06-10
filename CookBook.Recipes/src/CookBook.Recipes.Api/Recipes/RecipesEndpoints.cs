using CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;
using CookBook.Recipes.Api.Recipes.Features.RemoveRecipe;
using CookBook.Recipes.Api.Recipes.Features.SaveRecipe;
using CookBook.Recipes.Api.Recipes.Features.SearchRecipes;

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
        GetRecipeDetailEndpoint.Configure(recipesGroup);

        return group;
    }
}
