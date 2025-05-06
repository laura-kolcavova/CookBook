using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;

namespace CookBook.Recipes.Api.Categories.Features.GetCategories;

internal static class GetCategoriesEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapGet("", HandleAsync)
            .WithName("GetCategories")
            .WithSummary("Get categories")
            .WithDescription("Returns a DTO containing a collection of category listing items")
            .Produces<GetCategoriesResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetCategoriesRequestDto request,
        IGetCategoriesService getCategoriesService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var categories = await getCategoriesService.GetCategories(
            request.ParentCategoryId ?? CategoryAggregate.RootCategoryId,
            cancellationToken);

        return TypedResults.Ok(new GetCategoriesResponseDto
        {
            Categories = categories
        });
    }
}
