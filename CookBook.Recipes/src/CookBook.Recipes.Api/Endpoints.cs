using CookBook.Recipes.Api.Categories;
using CookBook.Recipes.Api.Recipes;

namespace CookBook.Recipes.Api;

internal static class Endpoints
{
    public static RouteGroupBuilder UseEndpoints(this WebApplication app)
    {
        return app.MapGroup("/api/")
            .AddCategoriesEndpoints()
            .AddRecipesEndpoints();
    }
}
