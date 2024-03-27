using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class RecipeDetailReadModelService : IRecipeDetailReadModelService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<RecipeDetailReadModelService> _logger;

    public RecipeDetailReadModelService(
        RecipesContext recipesContext,
        ILogger<RecipeDetailReadModelService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<RecipeDetailReadModel?> GetRecipeDetailAsync(long recipeId, CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = recipeId
        });

        try
        {
            return await _recipesContext.Recipes
                .AsNoTracking()
                .ProjectToRecipeDetailReadModel()
                .SingleOrDefaultAsync(recipeDetail =>
                    recipeDetail.Id == recipeId,
                    cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting recipe detail");
            throw;
        }
    }
}
