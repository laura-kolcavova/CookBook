using CookBook.Extensions.AspNetCore.Extensions;
using CookBook.Recipes.Application.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.MoveCategory;

internal static class MoveCategoryEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapPut("move", HandleAsync)
            .WithName("MoveCategory")
            .WithSummary("Moves a category to a different parent category")
            .WithDescription("Returns Status 200 OK response if category was successfully moved to a different parent category.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody]
        MoveCategoryRequestDto request,
        ICategoryCommandService categoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var moveCategoryResult = await categoryService
            .MoveCategoryAsync(
                request.CategoryId,
                request.NewParentCategoryId,
                cancellationToken);

        if (moveCategoryResult.IsFailure)
        {
            return TypedResults.Problem(
                moveCategoryResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok();
    }
}
