using CookBook.Extensions.AspNetCore.Errors;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;

internal static class RemoveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapDelete("/{recipeId}/Remove", HandleAsync)
            .WithName("RemoveRecipe")
            .WithSummary("Removes a recipe by its id")
            .WithDescription("This endpoint returns Status 200 OK response if recipe was successfully removed.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        RemoveRecipeEndpointParams request,
        IRemoveRecipeService removeRecipeService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var removeRecipeResult = await removeRecipeService.RemoveRecipe(
            request.RecipeId,
            request.UserId,
            cancellationToken);

        if (removeRecipeResult.IsFailure)
        {
            return TypedResults.Problem(
                removeRecipeResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.Ok();
    }
}
