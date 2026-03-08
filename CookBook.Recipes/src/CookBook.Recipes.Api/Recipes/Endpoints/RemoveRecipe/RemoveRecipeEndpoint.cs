using CookBook.Extensions.AspNetCore.Errors;
using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Infrastructure.Shared.Configuration;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;

internal static class RemoveRecipeEndpoint
{
    public static void Configure(IEndpointRouteBuilder builder)
    {
        builder
            .MapDelete("/{recipeId}/remove", HandleAsync)
            .WithName("RemoveRecipe")
            .WithSummary("Removes a recipe by its id")
            .WithDescription("This endpoint returns Status 204 OK response if recipe was successfully removed.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.OpenIdConnect);
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
            request.UserName,
            cancellationToken);

        if (removeRecipeResult.IsFailure)
        {
            return TypedResults.Problem(
                removeRecipeResult.Error.AsProblemDetails(httpContext));
        }

        return TypedResults.NoContent();
    }
}
