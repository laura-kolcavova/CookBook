using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.Extensions.AspNetCore.FluentValidation.Extensions;
using CookBook.Recipes.Api.Recipes;

namespace CookBook.Recipes.Api.Shared;

internal static class ApiEndpoints
{
    public static RouteGroupBuilder MapApiEndpoints(
        this WebApplication app)
    {
        return app
            .MapGroup("/api")
            .AddFluentValidation()
            .AddClosedRequest()
            .MapRecipesEndpoints()
            .WithOpenApi(); // This is not needed since newer versions of Swashbuckle.AspNetCore
    }
}
