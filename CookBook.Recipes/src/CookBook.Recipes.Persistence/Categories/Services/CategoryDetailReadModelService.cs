using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories.ReadModels;
using CookBook.Recipes.Persistence.Categories.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class CategoryDetailReadModelService : ICategoryDetailReadModelService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<CategoryDetailReadModelService> _logger;

    public CategoryDetailReadModelService(
        RecipesContext recipesContext,
        ILogger<CategoryDetailReadModelService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<CategoryDetailReadModel?> GetCategoryDetailAsync(int categoryId, CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["CategoryId"] = categoryId
        }))
        {
            try
            {
                var categoryDetail = await _recipesContext.Categories
                    .AsNoTracking()
                    .Where(category => category.Id == categoryId)
                    .ProjectToCategoryDetailReadModel()
                    .SingleOrDefaultAsync(cancellationToken);

                if (categoryDetail is null)
                {
                    return null;
                }

                var subCategories = await _recipesContext.Categories
                    .AsNoTracking()
                    .Where(category => category.ParentCategoryId == categoryId)
                    .OrderBy(category => category.Name)
                    .ProjectToCategoryListingItemReadModel()
                    .ToListAsync(cancellationToken);

                return categoryDetail with
                {
                    SubCategories = subCategories
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting a main category detail");
                throw;
            }
        }
    }
}
