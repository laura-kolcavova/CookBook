using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class RemoveRecipeService(
    RecipesContext recipesContext,
    ILogger<RemoveRecipeService> logger) :
    IRemoveRecipeService
{
    public async Task<UnitResult<Error>> RemoveRecipe(
        long recipeId,
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
                return Domain.Recipes.Errors.Recipe.NotFound(recipeId);
            }

            recipesContext
                .Recipes
                .Remove(recipe);

            await recipesContext.SaveChangesAsync(cancellationToken);

            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while removing a recipe");
        }
    }
}
