using CookBook.Extensions.AspNetCore.Utilities;
using CookBook.Recipes.Application.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.AddCategory;

internal static class AddCategoryEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapPost("add", HandleAsync)
            .WithName("AddCategory")
            .WithSummary("Adds a category")
            .WithDescription("Adds category and returns DTO containing a category id")
            .Produces<AddCategoryResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] AddCategoryRequestDto request,
        ICategoryCommandService categoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var addCategoryResult = await categoryService
            .AddCategoryAsync(request.Name, request.ParentCategoryId, cancellationToken);

        if (addCategoryResult.IsFailure)
        {
            return EndpointResults.Problem(addCategoryResult.Error, httpContext);
        }

        return Results.Ok(new AddCategoryResponseDto
        {
            CategoryId = addCategoryResult.Value.CategoryId,
        });
    }
}
