using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Categories.ReadModels;

public sealed record CategoryListingItemReadModel : IReadModel
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}
