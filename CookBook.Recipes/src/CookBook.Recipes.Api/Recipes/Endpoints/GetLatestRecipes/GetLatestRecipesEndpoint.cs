using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Contracts;
using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Mappers;
using CookBook.Recipes.Application.Recipes.UseCases.GetLatestRecipes;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes;

internal static class GetLatestRecipesEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("/latest", HandleAsync)
            .WithName("GetLatestRecipes")
            .WithSummary("Gets latest recipes")
            .WithDescription("Returns a collection of latest recipes")
            .Produces<GetLatestRecipesResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetLatestRecipesEndpointParams request,
        IGetLatestRecipesUseCase getLatestRecipesUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var latestRecipes = await getLatestRecipesUseCase.GetLatestRecipes(
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
}
