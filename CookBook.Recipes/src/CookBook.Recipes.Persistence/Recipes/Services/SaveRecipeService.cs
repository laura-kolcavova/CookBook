using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Recipes;
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
    public async Task<Result<SaveRecipeResultModel, Error>> SaveRecipe(
        SaveRecipeRequestModel request,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.RecipeId,
            ["UserId"] = request.UserId,
            ["Title"] = request.Title,
        });

        try
        {
            var recipe =
                request.RecipeId is null ||
                request.RecipeId <= 0
                ? null
                : await recipesContext
                    .Recipes
                    .FetchRecipeAsync(
                        request.RecipeId.Value,
                        cancellationToken);

            if (recipe is null)
            {
                recipe = new RecipeAggregate(
                    request.Title,
                    request.UserId);

                SaveRecipeOptionalInformation(
                    recipe,
                    request,
                    cancellationToken);

                await recipesContext
                    .Recipes
                    .AddAsync(recipe, cancellationToken);
            }
            else
            {
                recipe.SetTitle(
                    request.Title);

                SaveRecipeOptionalInformation(
                    recipe,
                    request,
                    cancellationToken);

                recipesContext
                    .Recipes
                    .Update(recipe);
            }

            await recipesContext.SaveChangesAsync(
                cancellationToken);

            return new SaveRecipeResultModel
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
        SaveRecipeRequestModel request,
        CancellationToken cancellationToken)
    {
        recipe.SetDescription(request.Description);
        recipe.SetServings(request.Servings);
        recipe.SetPreparationTime(request.PreparationTime);
        recipe.SetCookTime(request.CookTime);
        recipe.SetNotes(request.Notes);
        recipe.SaveIngredients(request.Ingredients);
        recipe.SaveInstructions(request.Instructions);
        recipe.SaveTags(request.Tags);
    }
}
