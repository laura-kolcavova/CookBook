using CookBook.Extensions.AspNetCore.Errors.Extensions;
using CookBook.Recipes.Api.Shared.Extensions;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Infrastructure.Shared.Configuration;
using System.Security.Claims;
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
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.ReadWrite)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        RemoveRecipeParams request,
        IRecipeStore recipeStore,
        ILogger<RemoveRecipeEndpoint> logger,
        ClaimsPrincipal claimsPrincipal,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.RecipeId,
        });

        try
        {
            var userName = claimsPrincipal.GetUserNameClaim().Value;

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

            if (recipe.UserName != userName)
            {
                return TypedResults.Problem(
                    RecipeErrors
                        .Recipe
                        .NotOwnedByUser(
                            request.RecipeId,
                            userName)
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
