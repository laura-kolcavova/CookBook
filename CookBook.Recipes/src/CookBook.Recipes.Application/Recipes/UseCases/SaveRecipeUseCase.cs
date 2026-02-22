using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Models;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Recipes.UseCases;

internal sealed class SaveRecipeUseCase(
    IRecipeStore recipeStore,
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
            ["UserName"] = saveRecipeParams.UserName,
            ["Title"] = saveRecipeParams.Title,
        });

        try
        {
            var recipe =
                saveRecipeParams.RecipeId is null ||
                saveRecipeParams.RecipeId <= 0
                ? null
                : await recipeStore.FindByRecipeId(
                    saveRecipeParams.RecipeId.Value,
                    cancellationToken);

            if (recipe is null)
            {
                recipe = CreateRecipe(saveRecipeParams);

                await recipeStore.Add(
                    recipe,
                    cancellationToken);

                return new SaveRecipeResult
                {
                    RecipeId = recipe.Id
                };
            }

            if (recipe.UserName != saveRecipeParams.UserName)
            {
                return RecipeErrors.Recipe.NotOwnedByUser(
                    recipe.Id,
                    saveRecipeParams.UserName);
            }

            UpdateRecipe(recipe, saveRecipeParams);

            await recipeStore.Update(
                recipe,
                cancellationToken);

            return new SaveRecipeResult
            {
                RecipeId = recipe.Id
            };
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while saving a recipe");

            throw;
        }
    }

    private static RecipeAggregate CreateRecipe(
        SaveRecipeParams saveRecipeParams)
    {
        var recipe = new RecipeAggregate(
            saveRecipeParams.Title,
            saveRecipeParams.UserName);

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
