using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Categories.ReadModels;

public sealed record CategoryDetailReadModel : IReadModel
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required int ParentCategoryId { get; init; }

    public IReadOnlyCollection<CategoryListingItemReadModel> SubCategories { get; init; } = new List<CategoryListingItemReadModel>();
}
