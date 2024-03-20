using CookBook.Extensions.AspNetCore.Utilities;
using CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;
using CookBook.Recipes.Application.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.AddMainCategory;

internal static class AddMainCategoryEndpoint
{
    public static void Configure(RouteGroupBuilder categoriesGroup)
    {
        categoriesGroup
            .MapPost("add-main", HandleAsync)
            .WithName("AddMainCategory")
            .WithSummary("Adds main category")
            .WithDescription("Adds main category and returns DTO with category id")
            .Produces<GetRecipeDetailResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] AddMainCategoryRequestDto request,
        ICategoryService categoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var addMainCategoryResult = await categoryService
            .AddMainCategory(request.Name, cancellationToken);

        if (addMainCategoryResult.IsFailure)
        {
            return EndpointResults.Problem(addMainCategoryResult.Error, httpContext);
        }

        return Results.Ok(new AddMainCategoryResponseDto
        {
            CategoryId = addMainCategoryResult.Value.CategoryId,
        });
    }
}
