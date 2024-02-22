using CookBook.Extensions.AspNetCore.Utilities;
using CookBook.Recipes.Application.Features.Recipes.RemoveRecipe;
using MediatR;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Features.Recipes.RemoveRecipe;

internal static class RemoveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapDelete("{recipeId}", HandleAsync)
            .WithName("RemoveRecipe")
            .WithSummary("Removes a recipe by its id")
            .WithDescription("This endpoint returns Status 200 OK response if recipe was successfully removed.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] RemoveRecipeRequestDto request,
        IMediator mediator,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var removeRecipeResult = await mediator.Send(new RemoveRecipeCommand()
        {
            RecipeId = request.RecipeId
        }, cancellationToken);

        if (removeRecipeResult.IsFailure)
        {
            return EndpointResults.Problem(removeRecipeResult.Error, httpContext);
        }

        return Results.Ok();
    }
}
