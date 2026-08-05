using CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.RecipesWebapp.Server.Domain.Recipes.Services.Abastractions;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail;

public sealed class GetRecipeDetailEndpoint :
    RecipesModule
{
    public override void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app
           .MapGet("/{recipeId}/detail", HandleAsync)
           .WithName("GetRecipeDetail")
           .WithSummary("Gets recipe detail by its id")
           .WithDescription("Returns a DTO containing recipe detail")
           .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status204NoContent)
           .ProducesValidationProblem()
           .ProducesProblem(StatusCodes.Status500InternalServerError)
           .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetRecipeDetailParams request,
        IRecipeDetailFetcher recipeDetailFetcher,
        ILogger<GetRecipeDetailEndpoint> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.RecipeId,
        });

        try
        {
            var recipeDetailResult = await recipeDetailFetcher.FetchRecipeDetail(
                request.RecipeId,
                cancellationToken);

            if (recipeDetailResult.HasNoValue)
            {
                return TypedResults.NoContent();
            }

            var recipeDetail = recipeDetailResult.Value;

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
