using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class RecipeListingItemReadModelService : IRecipeListingItemReadModelService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<RecipeListingItemReadModelService> _logger;

    public RecipeListingItemReadModelService(
        RecipesContext recipesContext,
        ILogger<RecipeListingItemReadModelService> logger)
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
                .AsNoTracking()
                .ProjectToRecipeListingItemReadModel();

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
               .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while searching for recipes");
            throw;
        }
    }
}
