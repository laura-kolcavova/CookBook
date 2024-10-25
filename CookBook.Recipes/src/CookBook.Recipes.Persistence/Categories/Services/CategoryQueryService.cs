using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Categories.ReadModels;
using CookBook.Recipes.Persistence.Categories.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class CategoryQueryService : ICategoryQueryService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<CategoryQueryService> _logger;

    public CategoryQueryService(
        RecipesContext recipesContext,
        ILogger<CategoryQueryService> logger)
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
                throw RecipesPersistenceException.LogAndCreate(
                    _logger,
                    ex,
                    "An unexpected error occurred while getting a main category detail");
            }
        }
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
                throw RecipesPersistenceException.LogAndCreate(
                    _logger,
                    ex,
                    "An unexpected error occurred while searching for categories");
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
