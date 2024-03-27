using CookBook.Recipes.Application.Recipes.Services;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;

internal static class GetRecipeDetailEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapGet("{recipeId}/detail", HandleAsync)
            .WithName("GetRecipeDetail")
            .WithSummary("Gets recipe detail by its id")
            .WithDescription("Returns a DTO containing recipe detail")
            .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
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
