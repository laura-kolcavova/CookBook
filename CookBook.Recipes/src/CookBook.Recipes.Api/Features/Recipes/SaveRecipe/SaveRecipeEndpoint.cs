using CookBook.Extensions.AspNetCore.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

internal static class SaveRecipeEndpoint
{
    public static async Task<IResult> HandleAsync(
        [FromBody] SaveRecipeRequestDto saveRecipeRequest,
        IMediator mediator,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var result = await mediator
            .Send(SaveRecipeMapper.ToCommand(saveRecipeRequest), cancellationToken);

        if (result.IsFailure)
        {
            return EndpointResults.Problem(result.Error, httpContext);
        }

        return Results.Ok(SaveRecipeMapper.ToResponse(result.Value));
    }
}
