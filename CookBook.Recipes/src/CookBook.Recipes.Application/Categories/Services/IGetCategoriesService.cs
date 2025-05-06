using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Application.Categories.Services;

public interface IGetCategoriesService
{
    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategories(
       int parentCategoryId,
       CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<CategoryListingItemReadModel>> GetCategories(
       CancellationToken cancellationToken);
}
