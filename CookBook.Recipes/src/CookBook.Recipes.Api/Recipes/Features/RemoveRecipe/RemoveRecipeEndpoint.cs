﻿using CookBook.Extensions.AspNetCore.Extensions;
using CookBook.Recipes.Application.Recipes.Services;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Recipes.Features.RemoveRecipe;

internal static class RemoveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapDelete("{recipeId}/remove", HandleAsync)
            .WithName("RemoveRecipe")
            .WithSummary("Removes a recipe by its id")
            .WithDescription("This endpoint returns Status 200 OK response if recipe was successfully removed.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] RemoveRecipeRequestDto request,
        IRecipeCommandService recipeService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var removeRecipeResult = await recipeService.RemoveRecipeAsync(
            request.RecipeId,
            cancellationToken);

        if (removeRecipeResult.IsFailure)
        {
            return TypedResults.Problem(
                removeRecipeResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok();
    }
}
