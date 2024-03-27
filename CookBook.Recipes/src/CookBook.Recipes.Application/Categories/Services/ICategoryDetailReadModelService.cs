using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Application.Categories.Services;

public interface ICategoryDetailReadModelService
{
    public Task<CategoryDetailReadModel?> GetCategoryDetailAsync(
        int categoryId,
        CancellationToken cancellationToken);
}
