using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Extensions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class RecipeService : IRecipeService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<RecipeService> _logger;

    public RecipeService(
        RecipesContext recipesContext,
        ILogger<RecipeService> logger)
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
                return RecipeErrors.NotFound(recipeId);
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
                SaveRecipeOptionalInformation(recipe, request);
                await _recipesContext.Recipes.AddAsync(recipe, cancellationToken);
            }
            else
            {
                recipe.SetTitle(request.Title);
                SaveRecipeOptionalInformation(recipe, request);
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

    private static void SaveRecipeOptionalInformation(
        RecipeAggregate recipe,
        SaveRecipeRequest request)
    {
        recipe.SetDescription(request.Description);
        recipe.SetServings(request.Servings);
        recipe.SetPreparationTime(request.PreparationTime);
        recipe.SetCookTime(request.CookTime);
        recipe.SetNotes(request.Notes);
        recipe.SaveIngredients(request.Ingredients);
        recipe.SaveInstructions(request.Instructions);
    }
}
