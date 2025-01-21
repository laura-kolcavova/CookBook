using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories.ReadModels;
using CookBook.Recipes.Persistence.Categories.Extensions;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class GetCategoryDetailService(
    CategoriesContext categoriesContext,
    ILogger<GetCategoryDetailService> logger) :
    IGetCategoryDetailService
{
    public async Task<CategoryDetailReadModel?> GetCategoryDetail(
        int categoryId,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["CategoryId"] = categoryId
        }))
        {
            try
            {
                var categoryDetail = await categoriesContext
                    .Categories
                    .AsNoTracking()
                    .Where(category => category.Id == categoryId)
                    .ProjectToCategoryDetailReadModel()
                    .SingleOrDefaultAsync(cancellationToken);

                if (categoryDetail is null)
                {
                    return null;
                }

                var subCategories = await categoriesContext
                    .Categories
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
                    logger,
                    ex,
                    "An unexpected error occurred while getting a main category detail");
            }
        }
    }
}
