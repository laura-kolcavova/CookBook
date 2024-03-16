namespace CookBook.Recipes.Domain.Recipes;

public readonly struct RecipeIngredientPrimaryKey
{
    public required long RecipeId { get; init; }

    public required int LocalId { get; init; }
}
