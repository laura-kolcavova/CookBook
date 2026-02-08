using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Recipes;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.UseCases;

internal sealed class RemoveRecipeUseCase(
    RecipesContext recipesContext,
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
            var recipe = await recipesContext
                .Recipes
                .FindAsync(recipeId, cancellationToken);

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

            recipesContext
                .Recipes
                .Remove(recipe);

            await recipesContext.SaveChangesAsync(
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
