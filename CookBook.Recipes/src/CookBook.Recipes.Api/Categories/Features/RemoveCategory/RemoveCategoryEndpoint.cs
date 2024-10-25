using CookBook.Extensions.AspNetCore.Extensions;
using CookBook.Recipes.Application.Categories.Services;

namespace CookBook.Recipes.Api.Categories.Features.RemoveCategory;

internal static class RemoveCategoryEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapDelete("{categoryId}/remove", HandleAsync)
            .WithName("RemoveCategory")
            .WithSummary("Removes a category")
            .WithDescription("Returns Status 200 OK response if category was successfully removed.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        RemoveCategoryRequestDto request,
        ICategoryCommandService categoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var removeCategoryResult = await categoryService
            .RemoveCategoryAsync(
                request.CategoryId,
                cancellationToken);

        if (removeCategoryResult.IsFailure)
        {
            return TypedResults.Problem(
                removeCategoryResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok();
    }
}
