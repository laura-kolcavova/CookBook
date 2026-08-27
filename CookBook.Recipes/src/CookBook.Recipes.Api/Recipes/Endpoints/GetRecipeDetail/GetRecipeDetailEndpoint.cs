using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Mappers;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail;

internal class GetRecipeDetailEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("/{recipeId}/detail", HandleAsync)
            .WithName("GetRecipeDetail")
            .WithSummary("Gets recipe detail by its id")
            .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetRecipeDetailParams request,
        IGetRecipeDetailQuery getRecipeDetailQuery,
        ILogger<GetRecipeDetailEndpoint> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.RecipeId
        });

        try
        {
            var recipeDetail = await getRecipeDetailQuery.Execute(
                request.RecipeId,
                cancellationToken);

            if (recipeDetail is null)
            {
                return TypedResults.NoContent();
            }

            var responseDto = new GetRecipeDetailResponseDto
            {
                RecipeDetail = recipeDetail
                    .ToDto()
            };

            return TypedResults.Ok(responseDto);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting recipe detail");

            throw;
        }
    }
}
