using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class RecipeQueryService : IRecipeQueryService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<RecipeQueryService> _logger;

    public RecipeQueryService(
        RecipesContext recipesContext,
        ILogger<RecipeQueryService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<RecipeListingItemReadModel>> SearchRecipesAsync(
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _recipesContext.Recipes
                .AsNoTracking();

            if (sorting is not null)
            {
                queryable = queryable
                    .SortBy(sorting);
            }

            if (offsetFilter is not null)
            {
                queryable = queryable
                    .Skip(offsetFilter.Offset)
                    .Take(offsetFilter.Limit);
            }

            return await queryable
               .ProjectToRecipeListingItemReadModel()
               .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while searching for recipes");
            throw;
        }
    }

    public async Task<RecipeDetailReadModel?> GetRecipeDetailAsync(long recipeId, CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = recipeId
        });

        try
        {
            var readModel = await _recipesContext.Recipes
                .AsNoTracking()
                .ProjectToRecipeDetailReadModel()
                .SingleOrDefaultAsync(recipeDetail =>
                    recipeDetail.Id == recipeId,
                    cancellationToken);

            if (readModel == null)
            {
                return null;
            }

            var categoryIds = readModel.Categories
                .Select(category => category.Id);

            var categories = await _recipesContext.Categories
                .Where(category => categoryIds.Contains(category.Id))
                .Select(category => new RecipeDetailReadModel.CategoryItem
                {
                    Id = category.Id,
                    Name = category.Name,
                })
                .ToListAsync(cancellationToken);

            readModel = readModel with
            {
                Categories = categories
            };

            return readModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting recipe detail");
            throw;
        }
    }
}
