using CookBook.Extensions.AspNetCore.FluentValidation;
using CookBook.Extensions.AspNetCore.Shared;
using CookBook.Recipes.Api.Recipes;

namespace CookBook.Recipes.Api.Shared;

internal static class ApiEndpoints
{
    public static RouteGroupBuilder MapApiEndpoints(
        this WebApplication app)
    {
        return app
            .MapGroup("/api")
            .AddEndpointFilter<OperationCanceledExceptionFilter>()
            .AddEndpointFilter<FluentValidationEndpointFilter>()
            .MapRecipesEndpoints()
            .WithOpenApi(); // This is not needed since newer versions of Swashbuckle.AspNetCore
    }
}
