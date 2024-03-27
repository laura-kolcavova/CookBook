namespace CookBook.Recipes.Domain.Categories.ReadModels;

public sealed record CategoryListingItemReadModel
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}
