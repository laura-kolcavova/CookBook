using CookBook.Extensions.AspNetCore.Errors;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;

internal static class RemoveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapDelete("/{recipeId}/remove", HandleAsync)
            .WithName("RemoveRecipe")
            .WithSummary("Removes a recipe by its id")
            .WithDescription("This endpoint returns Status 204 OK response if recipe was successfully removed.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        RemoveRecipeEndpointParams request,
        IRemoveRecipeUseCase removeRecipeUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var removeRecipeResult = await removeRecipeUseCase.RemoveRecipe(
            request.RecipeId,
            request.UserId,
            cancellationToken);

        if (removeRecipeResult.IsFailure)
        {
            return TypedResults.Problem(
                removeRecipeResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.NoContent();
    }
}
