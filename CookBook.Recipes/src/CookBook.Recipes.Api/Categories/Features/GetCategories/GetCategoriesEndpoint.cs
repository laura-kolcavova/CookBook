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
        ICategoryListingItemReadModelService categoryListingItemReadModel,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var categories = await categoryListingItemReadModel.GetCategoriesAsync(
            request.CategoryId ?? CategoryAggregate.RootCategoryId,
            cancellationToken);

        return Results.Ok(new GetCategoriesResponseDto
        {
            Categories = categories
        });
    }
}
