using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail;

internal static class GetRecipeDetailEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapGet("/{recipeId}/detail", HandleAsync)
            .WithName("GetRecipeDetail")
            .WithSummary("Gets recipe detail by its id")
            .WithDescription("Returns a DTO containing recipe detail")
            .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetRecipeDetailEndpointParams request,
        IGetRecipeDetailUseCase getRecipeDetailUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var recipeDetail = await getRecipeDetailUseCase.GetRecipeDetail(
            request.RecipeId,
            cancellationToken);

        if (recipeDetail.HasNoValue)
        {
            return TypedResults.NoContent();
        }

        var responseDto = new GetRecipeDetailResponseDto
        {
            RecipeDetail = recipeDetail.Value
        };

        return TypedResults.Ok(responseDto);
    }
}
