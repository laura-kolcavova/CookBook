using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Models;
using CookBook.Recipes.Domain.Recipes.Services;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class SaveRecipeService(
    RecipesContext recipesContext,
    ILogger<SaveRecipeService> logger) :
    ISaveRecipeService
{
    public async Task<Result<SaveRecipeResult, Error>> SaveRecipe(
        SaveRecipeParams saveRecipeParams,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = saveRecipeParams.RecipeId,
            ["UserId"] = saveRecipeParams.UserId,
            ["Title"] = saveRecipeParams.Title,
        });

        try
        {
            var recipe =
                saveRecipeParams.RecipeId is null ||
                saveRecipeParams.RecipeId <= 0
                ? null
                : await recipesContext
                    .Recipes
                    .FetchRecipeAsync(
                        saveRecipeParams.RecipeId.Value,
                        cancellationToken);

            if (recipe is null)
            {
                recipe = new RecipeAggregate(
                    saveRecipeParams.Title,
                    saveRecipeParams.UserId);

                SaveRecipeOptionalInformation(
                    recipe,
                    saveRecipeParams,
                    cancellationToken);

                await recipesContext
                    .Recipes
                    .AddAsync(recipe, cancellationToken);
            }
            else
            {
                recipe.SetTitle(
                    saveRecipeParams.Title);

                SaveRecipeOptionalInformation(
                    recipe,
                    saveRecipeParams,
                    cancellationToken);

                recipesContext
                    .Recipes
                    .Update(recipe);
            }

            await recipesContext.SaveChangesAsync(
                cancellationToken);

            return new SaveRecipeResult
            {
                RecipeId = recipe.Id
            };
        }
        catch (Exception ex)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while saving a recipe");
        }
    }

    private void SaveRecipeOptionalInformation(
        RecipeAggregate recipe,
        SaveRecipeParams saveRecipeParams,
        CancellationToken cancellationToken)
    {
        recipe.SetDescription(saveRecipeParams.Description);
        recipe.SetServings(saveRecipeParams.Servings);
        recipe.SetPreparationTime(saveRecipeParams.PreparationTime);
        recipe.SetCookTime(saveRecipeParams.CookTime);
        recipe.SetNotes(saveRecipeParams.Notes);
        recipe.SaveIngredients(saveRecipeParams.Ingredients);
        recipe.SaveInstructions(saveRecipeParams.Instructions);
        recipe.SaveTags(saveRecipeParams.Tags);
    }
}
