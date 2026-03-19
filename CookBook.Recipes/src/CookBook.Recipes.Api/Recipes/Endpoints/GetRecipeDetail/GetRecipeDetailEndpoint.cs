using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Mappers;
using CookBook.Recipes.Application.Recipes.UseCases.GetRecipeDetail;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail;

internal static class GetRecipeDetailEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
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
        GetRecipeDetailEndpointParams request,
        IGetRecipeDetailUseCase getRecipeDetailUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var recipeDetailResult = await getRecipeDetailUseCase.GetRecipeDetail(
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
}
