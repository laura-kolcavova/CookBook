using CookBook.Extensions.AspNetCore.Errors.Extensions;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Infrastructure.Shared.Configuration;
using IResult = Microsoft.AspNetCore.Http.IResult;
using RecipeErrors = CookBook.Recipes.Domain.Recipes.RecipeErrors;

namespace CookBook.Recipes.Api.Recipes.Endpoints.RemoveRecipe;

internal class RemoveRecipeEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
            .MapDelete("/{recipeId}/remove", HandleAsync)
            .WithName("RemoveRecipe")
            .WithSummary("Removes a recipe by its id")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.ReadWrite);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        RemoveRecipeParams request,
        IRecipeStore recipeStore,
        ILogger<RemoveRecipeEndpoint> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.RecipeId,
            ["UserName"] = request.UserName,
        });

        try
        {
            var recipe = await recipeStore.FindByRecipeId(
                request.RecipeId,
                cancellationToken);

            if (recipe is null)
            {
                return TypedResults.Problem(
                    RecipeErrors
                        .Recipe
                        .NotFound(
                            request.RecipeId)
                        .AsProblemDetails(httpContext));
            }

            if (recipe.UserName != request.UserName)
            {
                return TypedResults.Problem(
                    RecipeErrors
                        .Recipe
                        .NotOwnedByUser(
                            request.RecipeId,
                            request.UserName)
                        .AsProblemDetails(httpContext));
            }

            await recipeStore.Remove(
                recipe,
                cancellationToken);

            return TypedResults.NoContent();
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while removing a recipe");

            throw;
        }
    }
}
