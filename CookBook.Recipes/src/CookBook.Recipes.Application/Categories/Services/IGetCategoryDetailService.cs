using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Application.Categories.Services;

public interface IGetCategoryDetailService
{
    public Task<CategoryDetailReadModel?> GetCategoryDetail(
        int categoryId,
        CancellationToken cancellationToken);
}
