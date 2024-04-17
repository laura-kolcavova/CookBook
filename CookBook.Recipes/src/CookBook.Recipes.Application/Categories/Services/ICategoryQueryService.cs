using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Application.Categories.Services;

public interface ICategoryQueryService
{
    public Task<CategoryDetailReadModel?> GetCategoryDetailAsync(
        int categoryId,
        CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategoriesAsync(
       int parentCategoryId,
       CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategoriesAsync(
       CancellationToken cancellationToken);
}
