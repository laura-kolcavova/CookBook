using CookBook.Recipes.Api.Features.Recipes;

namespace CookBook.Recipes.Api;

internal static class Endpoints
{
    public static RouteGroupBuilder AddEndpoints(this WebApplication app)
    {
        return app.MapGroup("/api/")
            .AddRecipesEndpoints();
    }
}

