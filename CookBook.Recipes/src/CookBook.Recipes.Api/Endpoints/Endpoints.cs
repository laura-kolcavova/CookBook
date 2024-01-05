namespace CookBook.Recipes.Api.Endpoints;

internal static class Endpoints
{
    public static RouteGroupBuilder AddEndpoints(this WebApplication app)
    {
        app.NewVersionedApi();
        return app.MapGroup("/api/");
    }
}

