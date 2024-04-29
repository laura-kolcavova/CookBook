using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes;

public sealed class RecipeCategoryEntity : Entity<RecipeCategoryPrimaryKey>
{
    public long RecipeId { get; }

    public int CategoryId { get; }

    public RecipeCategoryEntity(int categoryId)
    {
        CategoryId = categoryId;
    }

    public override RecipeCategoryPrimaryKey GetPrimaryKey() => new RecipeCategoryPrimaryKey
    {
        RecipeId = RecipeId,
        CategoryId = CategoryId,
    };
}
