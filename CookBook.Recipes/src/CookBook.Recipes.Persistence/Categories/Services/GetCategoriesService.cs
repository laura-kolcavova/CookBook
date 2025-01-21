using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Categories.ReadModels;
using CookBook.Recipes.Persistence.Categories.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class GetCategoriesService(
    RecipesContext recipesContext,
    ILogger<GetCategoriesService> logger) :
    IGetCategoriesService
{
    public async Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategories(
        int parentCategoryId,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["ParentCategoryId"] = parentCategoryId
        }))
        {
            try
            {
                var queryable = recipesContext
                    .Categories
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
                    logger,
                    ex,
                    "An unexpected error occurred while searching for categories");
            }
        }
    }

    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategories(
        CancellationToken cancellationToken)
    {
        return GetCategories(
            CategoryAggregate.RootCategoryId,
            cancellationToken);
    }
}
