namespace CookBook.Recipes.Domain.Recipes.Models;

public readonly struct RecipeTagPrimaryKey
{
    public required long RecipeId { get; init; }

    public required string Name { get; init; }
}
