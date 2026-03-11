using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Recipes.UseCases.RemoveRecipe;

internal sealed class RemoveRecipeUseCase(
    IRecipeStore recipeStore,
    ILogger<RemoveRecipeUseCase> logger) :
    IRemoveRecipeUseCase
{
    public async Task<UnitResult<Error>> RemoveRecipe(
        long recipeId,
        string userName,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = recipeId,
            ["UserName"] = userName,
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

            if (recipe.UserName != userName)
            {
                return RecipeErrors.Recipe.NotOwnedByUser(
                    recipeId,
                    userName);
            }

            await recipeStore.Delete(
                recipe,
                cancellationToken);

            return UnitResult.Success<Error>();
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
