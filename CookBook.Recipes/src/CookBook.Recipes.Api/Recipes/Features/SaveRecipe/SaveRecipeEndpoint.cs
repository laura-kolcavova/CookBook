﻿using CookBook.Extensions.AspNetCore.Extensions;
using CookBook.Recipes.Api.Features.Recipes.SaveRecipe;
using CookBook.Recipes.Application.Recipes.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe;

internal static class SaveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapPost("save", HandleAsync)
            .WithName("SaveRecipe")
            .WithSummary("Saves a newly created recipe or saves changes of already existing recipe")
            .WithDescription("This endpoint returns a DTO containing an id of created or edited recipe.")
            .Produces<SaveRecipeResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] SaveRecipeRequestDto saveRecipeRequest,
        ISaveRecipeService saveRecipeService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var saveRecipeResult = await saveRecipeService.SaveRecipe(
            SaveRecipeMapper.ToSaveRecipeRequest(saveRecipeRequest),
            cancellationToken);

        if (saveRecipeResult.IsFailure)
        {
            return TypedResults.Problem(
                saveRecipeResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok(
            SaveRecipeMapper.ToResponse(saveRecipeResult.Value));
    }
}
