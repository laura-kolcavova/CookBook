using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Models;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.UseCases;

internal sealed class SaveRecipeUseCase(
    RecipesContext recipesContext,
    ILogger<SaveRecipeUseCase> logger) :
    ISaveRecipeUseCase
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
                recipe = CreateRecipe(saveRecipeParams);

                await recipesContext
                    .Recipes
                    .AddAsync(recipe, cancellationToken);

                await recipesContext.SaveChangesAsync(
                    cancellationToken);

                return new SaveRecipeResult
                {
                    RecipeId = recipe.Id
                };
            }

            if (recipe.UserId != saveRecipeParams.UserId)
            {
                return RecipeErrors.Recipe.NotOwnedByUser(
                    recipe.Id,
                    saveRecipeParams.UserId);
            }

            UpdateRecipe(recipe, saveRecipeParams);

            recipesContext
                .Recipes
                .Update(recipe);

            await recipesContext.SaveChangesAsync(
                cancellationToken);

            return new SaveRecipeResult
            {
                RecipeId = recipe.Id
            };
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while saving a recipe");
        }
    }

    private static RecipeAggregate CreateRecipe(
        SaveRecipeParams saveRecipeParams)
    {
        var recipe = new RecipeAggregate(
            saveRecipeParams.Title,
            saveRecipeParams.UserId);

        recipe.SetDescription(saveRecipeParams.Description);
        recipe.SetServings(saveRecipeParams.Servings);
        recipe.SetCookTime(saveRecipeParams.CookTime);
        recipe.SetNotes(saveRecipeParams.Notes);
        recipe.SaveIngredients(saveRecipeParams.Ingredients);
        recipe.SaveInstructions(saveRecipeParams.Instructions);
        recipe.SaveTags(saveRecipeParams.Tags);

        return recipe;
    }

    private static void UpdateRecipe(
        RecipeAggregate recipe,
        SaveRecipeParams saveRecipeParams)
    {
        recipe.SetTitle(saveRecipeParams.Title);
        recipe.SetDescription(saveRecipeParams.Description);
        recipe.SetServings(saveRecipeParams.Servings);
        recipe.SetCookTime(saveRecipeParams.CookTime);
        recipe.SetNotes(saveRecipeParams.Notes);
        recipe.SaveIngredients(saveRecipeParams.Ingredients);
        recipe.SaveInstructions(saveRecipeParams.Instructions);
        recipe.SaveTags(saveRecipeParams.Tags);
    }
}
