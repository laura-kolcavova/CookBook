using CookBook.Recipes.Application.Categories.Services;

namespace CookBook.Recipes.Api.Categories.Features.GetCategoryDetail;

internal static class GetCategoryDetailEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapGet("{categoryId}/detail", HandleAsync)
            .WithName("GetCategoryDetail")
            .WithSummary("Gets category detail")
            .WithDescription("Returns a DTO containing a category detail")
            .Produces<GetCategoryDetailResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetCategoryDetailRequestDto request,
        ICategoryQueryService categoryDetailReadModelService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var categoryDetail = await categoryDetailReadModelService
            .GetCategoryDetailAsync(request.CategoryId, cancellationToken);

        if (categoryDetail is null)
        {
            return TypedResults.NoContent();

        }
        return Results.Ok(new GetCategoryDetailResponseDto
        {
            Category = categoryDetail
        });
    }
}
