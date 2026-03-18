using CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.RecipesWebapp.Server.Application.Recipes.UseCases.GetRecipeDetail;

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
