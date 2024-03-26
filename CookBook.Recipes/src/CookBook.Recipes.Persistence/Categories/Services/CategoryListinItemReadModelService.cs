using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Categories.ReadModels;
using CookBook.Recipes.Persistence.Categories.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class CategoryListinItemReadModelService : ICategoryListingItemReadModelService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<CategoryListinItemReadModelService> _logger;

    public CategoryListinItemReadModelService(
        RecipesContext recipesContext,
        ILogger<CategoryListinItemReadModelService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategoriesAsync(
        int parentCategoryId,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["ParentCategoryId"] = parentCategoryId
        }))
        {
            try
            {
                var queryable = _recipesContext.Categories
                    .AsNoTracking()
                    .Where(category => category.ParentCategoryId == parentCategoryId)
                    .OrderBy(category => category.Name)
                    .ProjectToCategoryListingItemReadModel();

                return await queryable
                   .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while searching for categories");
                throw;
            }
        }
    }

    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategoriesAsync(
        CancellationToken cancellationToken)
    {
        return GetCategoriesAsync(
            CategoryAggregate.RootCategoryId,
            cancellationToken);
    }
}
