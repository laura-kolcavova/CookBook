using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class SaveRecipeService(
    RecipesContext recipesContext,
    ILogger<SaveRecipeService> logger) :
    ISaveRecipeService
{
    public async Task<Result<SaveRecipeResult, Error>> SaveRecipe(
        SaveRecipeRequest request,
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
            var recipe = request.RecipeId is null || request.RecipeId <= 0
                ? null
                : await recipesContext
                    .Recipes
                    .FetchRecipeAsync(request.RecipeId.Value, cancellationToken);

            if (recipe is null)
            {
                recipe = new RecipeAggregate(request.Title, request.UserId);

                await SaveRecipeOptionalInformation(recipe, request, cancellationToken);

                await recipesContext
                    .Recipes
                    .AddAsync(recipe, cancellationToken);
            }
            else
            {
                recipe.SetTitle(request.Title);

                await SaveRecipeOptionalInformation(recipe, request, cancellationToken);

                recipesContext
                    .Recipes
                    .Update(recipe);
            }

            await recipesContext.SaveChangesAsync(cancellationToken);

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

    private async Task SaveRecipeOptionalInformation(
        RecipeAggregate recipe,
        SaveRecipeRequest request,
        CancellationToken cancellationToken)
    {
        var categories = Enumerable.Empty<CategoryAggregate>();

        if (request.CategoryIds.Any())
        {
            categories = await recipesContext
                .Categories
                .Where(category => request.CategoryIds.Contains(category.Id))
                .ToListAsync(cancellationToken);
        }

        recipe.SetDescription(request.Description);
        recipe.SetServings(request.Servings);
        recipe.SetPreparationTime(request.PreparationTime);
        recipe.SetCookTime(request.CookTime);
        recipe.SetNotes(request.Notes);
        recipe.SaveIngredients(request.Ingredients);
        recipe.SaveInstructions(request.Instructions);
        recipe.SaveCategories(categories);
        recipe.SaveTags(request.Tags);
    }
}
