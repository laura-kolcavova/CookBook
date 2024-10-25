using CookBook.Extensions.AspNetCore.Extensions;
using CookBook.Recipes.Application.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.RenameCategory;

internal static class RenameCategoryEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapPut("rename", HandleAsync)
            .WithName("RenameCategory")
            .WithSummary("Renames a category")
            .WithDescription("Returns Status 200 OK response if category was successfully renamed.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody]
        RenameCategoryRequestDto request,
        ICategoryCommandService categoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var renameCategoryResult = await categoryService
            .RenameCategoryAsync(
                request.CategoryId,
                request.Name,
                cancellationToken);

        if (renameCategoryResult.IsFailure)
        {
            return TypedResults.Problem(
                renameCategoryResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok();
    }
}
