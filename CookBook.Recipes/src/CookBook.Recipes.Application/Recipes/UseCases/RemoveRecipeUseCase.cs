using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Recipes.UseCases;

internal sealed class RemoveRecipeUseCase(
    IRecipeStore recipeStore,
    ILogger<RemoveRecipeUseCase> logger) :
    IRemoveRecipeUseCase
{
    public async Task<UnitResult<Error>> RemoveRecipe(
        long recipeId,
        int userId,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = recipeId
        });

        try
        {
            var recipe = await recipeStore.FindByRecipeId(
                recipeId,
                cancellationToken);

            if (recipe is null)
            {
                return RecipeErrors.Recipe.NotFound(
                    recipeId);
            }

            if (recipe.UserId != userId)
            {
                return RecipeErrors.Recipe.NotOwnedByUser(
                    recipeId,
                    userId);
            }

            await recipeStore.Delete(
                recipe,
                cancellationToken);

            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while removing a recipe");

            throw;
        }
    }
}
