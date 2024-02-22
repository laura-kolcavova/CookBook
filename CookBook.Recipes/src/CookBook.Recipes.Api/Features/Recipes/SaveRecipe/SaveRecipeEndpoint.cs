﻿using CookBook.Extensions.AspNetCore.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

internal static class SaveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapPost(string.Empty, HandleAsync)
            .WithName("SaveRecipe")
            .WithSummary("Saves a newly created recipe or saves changes of already existing recipe")
            .WithDescription("This endpoint returns a DTO containing an id of created or edited recipe.")
            .Produces<SaveRecipeResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] SaveRecipeRequestDto saveRecipeRequest,
        IMediator mediator,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var saveRecipeResult = await mediator
            .Send(SaveRecipeMapper.ToCommand(saveRecipeRequest), cancellationToken);

        if (saveRecipeResult.IsFailure)
        {
            return EndpointResults.Problem(saveRecipeResult.Error, httpContext);
        }

        return Results.Ok(SaveRecipeMapper.ToResponse(saveRecipeResult.Value));
    }
}
