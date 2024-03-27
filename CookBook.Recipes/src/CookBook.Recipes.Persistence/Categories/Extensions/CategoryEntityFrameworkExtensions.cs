using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Persistence.Categories.Extensions;

internal static class CategoryEntityFrameworkExtensions
{
    public static IQueryable<CategoryListingItemReadModel> ProjectToCategoryListingItemReadModel(
        this IQueryable<CategoryAggregate> categories)
    {
        return categories.Select(category => new CategoryListingItemReadModel
        {
            Id = category.Id,
            Name = category.Name,
        });
    }

    public static IQueryable<CategoryDetailReadModel> ProjectToCategoryDetailReadModel(
        this IQueryable<CategoryAggregate> categories)
    {
        return categories.Select(category => new CategoryDetailReadModel
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId,
        });
    }
}
