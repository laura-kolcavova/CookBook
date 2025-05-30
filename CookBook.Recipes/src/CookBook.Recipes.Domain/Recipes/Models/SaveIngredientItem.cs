namespace CookBook.Recipes.Domain.Recipes.Models;

public sealed record SaveIngredientItem
{
    public long? LocalId { get; init; } = default;

    public required string Note { get; init; }
}