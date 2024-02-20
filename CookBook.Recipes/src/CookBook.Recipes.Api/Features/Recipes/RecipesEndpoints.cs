using CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

namespace CookBook.Recipes.Api.Features.Recipes;

internal static class RecipesEndpoints
{
    public static RouteGroupBuilder AddRecipesEndpoints(this RouteGroupBuilder group)
    {
        var builder = group
            .MapGroup("/recipes")
            .WithTags("Recipes")
            .WithGroupName("api");

        builder
            .MapPost("fff", SaveRecipeEndpoint.HandleAsync);

        return group;
    }
}
