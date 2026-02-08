using CookBook.Extensions.AspNetCore.FluentValidation;
using CookBook.Extensions.AspNetCore.Shared;
using CookBook.Recipes.Api.Recipes;

namespace CookBook.Recipes.Api.Shared;

internal static class Endpoints
{
    public static RouteGroupBuilder MapEndpoints(this WebApplication app)
    {
        return app
            .MapGroup("/api")
            .AddEndpointFilter<OperationCanceledExceptionFilter>()
            .AddEndpointFilter<FluentValidationEndpointFilter>()
            .WithOpenApi()
            .MapRecipesEndpoints();
    }
}
