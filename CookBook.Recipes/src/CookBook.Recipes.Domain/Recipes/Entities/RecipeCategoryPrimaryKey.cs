namespace CookBook.Recipes.Domain.Recipes.Entities;

public readonly struct RecipeCategoryPrimaryKey
{
    public required long RecipeId { get; init; }

    public required int CategoryId { get; init; }
}
