using CookBook.Extensions.AspNetCore.Utilities;
using CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;
using CookBook.Recipes.Application.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.AddSubCategory;

internal sealed class AddSubCategoryEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapPost("add-sub", HandleAsync)
            .WithName("AddSubCategory")
            .WithSummary("Adds sub category")
            .WithDescription("Adds sub category and returns DTO with category id")
            .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] AddSubCategoryRequestDto request,
        ICategoryService categoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var addSubCategoryResult = await categoryService
            .AddSubCategory(request.Name, request.ParentCategoryId, cancellationToken);

        if (addSubCategoryResult.IsFailure)
        {
            return EndpointResults.Problem(addSubCategoryResult.Error, httpContext);
        }

        return Results.Ok(new AddSubCategoryResponseDto
        {
            CategoryId = addSubCategoryResult.Value.CategoryId,
        });
    }
}
