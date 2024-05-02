namespace CookBook.Recipes.Domain.Recipes.Entities;

public readonly struct RecipeIngredientPrimaryKey
{
    public required long RecipeId { get; init; }

    public required int LocalId { get; init; }
}
