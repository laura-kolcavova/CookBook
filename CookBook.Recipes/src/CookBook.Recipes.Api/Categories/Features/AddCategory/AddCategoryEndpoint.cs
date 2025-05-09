﻿using CookBook.Extensions.AspNetCore.Extensions;
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
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] AddCategoryRequestDto request,
        IAddCategoryService addCategoryService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var addCategoryResult = await addCategoryService.AddCategory(
            request.Name,
            request.ParentCategoryId,
            cancellationToken);

        if (addCategoryResult.IsFailure)
        {
            return TypedResults.Problem(
                addCategoryResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok(new AddCategoryResponseDto
        {
            CategoryId = addCategoryResult.Value.CategoryId,
        });
    }
}
