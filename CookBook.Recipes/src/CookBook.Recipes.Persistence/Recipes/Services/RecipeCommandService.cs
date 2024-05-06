using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Errors = CookBook.Recipes.Domain.Recipes.Errors;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class RecipeCommandService : IRecipeCommandService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<RecipeCommandService> _logger;

    public RecipeCommandService(
        RecipesContext recipesContext,
        ILogger<RecipeCommandService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<UnitResult<ExpectedError>> RemoveRecipeAsync(
        long recipeId,
        CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = recipeId
        });

        try
        {
            var recipe = await _recipesContext.Recipes
                .FindAsync(recipeId, cancellationToken);

            if (recipe is null)
            {
                return Errors.Recipe.NotFound(recipeId);
            }

            _recipesContext.Recipes.Remove(recipe);

            await _recipesContext.SaveChangesAsync(cancellationToken);

            return UnitResult.Success<ExpectedError>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while removing a recipe");
            throw;
        }
    }

    public async Task<Result<SaveRecipeResult, ExpectedError>> SaveRecipeAsync(
        SaveRecipeRequest request,
        CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.RecipeId,
            ["UserId"] = request.UserId,
            ["Title"] = request.Title,
        });

        try
        {
            var recipe = request.RecipeId is null || request.RecipeId <= 0
                ? null
                : await _recipesContext.Recipes
                    .FetchRecipeAsync(request.RecipeId.Value, cancellationToken);

            if (recipe is null)
            {
                recipe = new RecipeAggregate(request.Title, request.UserId);
                await SaveRecipeOptionalInformation(recipe, request, cancellationToken);
                await _recipesContext.Recipes.AddAsync(recipe, cancellationToken);
            }
            else
            {
                recipe.SetTitle(request.Title);
                await SaveRecipeOptionalInformation(recipe, request, cancellationToken);
                _recipesContext.Recipes.Update(recipe);
            }

            await _recipesContext.SaveChangesAsync(cancellationToken);

            return new SaveRecipeResult
            {
                RecipeId = recipe.Id
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while saving a recipe");
            throw;
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
            categories = await _recipesContext.Categories
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
