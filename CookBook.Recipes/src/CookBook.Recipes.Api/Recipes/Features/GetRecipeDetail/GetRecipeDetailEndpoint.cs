using CookBook.Recipes.Application.Recipes.Services;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;

internal static class GetRecipeDetailEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapGet("{recipeId}", HandleAsync)
            .WithName("GetRecipeDetail")
            .WithSummary("Gets recipe detail by its id")
            .WithDescription("This endpoint returns recipe detail DTO")
            .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] GetRecipeDetailRequestDto request,
        IRecipeDetailReadModelService recipeDetailReadModelService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var recipeDetail = await recipeDetailReadModelService
            .GetRecipeDetailAsync(request.RecipeId, cancellationToken);

        if (recipeDetail is null)
        {
            return TypedResults.NoContent();
        }

        return Results.Ok(new GetRecipeDetailResponseDto
        {
            RecipeDetail = recipeDetail
        });
    }
}
