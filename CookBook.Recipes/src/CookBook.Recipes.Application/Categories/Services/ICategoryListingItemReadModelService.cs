using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Application.Categories.Services;

public interface ICategoryListingItemReadModelService
{
    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategoriesAsync(
        int parentCategoryId,
        CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategoriesAsync(
       CancellationToken cancellationToken);
}
