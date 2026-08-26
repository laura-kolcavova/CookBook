using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Contracts;
using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Mappers;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes;

internal class GetLatestRecipesEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("/latest", HandleAsync)
            .WithName("GetLatestRecipes")
            .WithSummary("Gets latest recipes")
            .Produces<GetLatestRecipesResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetLatestRecipesParams request,
        IGetLatestRecipesQuery getLatestRecipesQuery,
        ILogger<GetLatestRecipesEndpoint> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["Count"] = request.Count
        });

        try
        {
            var latestRecipes = await getLatestRecipesQuery.Execute(
                request.Count,
                cancellationToken);

            if (latestRecipes.Count == 0)
            {
                return TypedResults.NoContent();
            }

            var responseDto = new GetLatestRecipesResponseDto
            {
                LatestRecipes = latestRecipes.ToDtoCollection(),
            };

            return TypedResults.Ok(responseDto);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting latest recipes");

            throw;
        }
    }
}
