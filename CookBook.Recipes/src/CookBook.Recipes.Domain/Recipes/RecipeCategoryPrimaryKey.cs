namespace CookBook.Recipes.Domain.Recipes;

public readonly struct RecipeCategoryPrimaryKey
{
    public required long RecipeId { get; init; }

    public required int CategoryId { get; init; }
}
