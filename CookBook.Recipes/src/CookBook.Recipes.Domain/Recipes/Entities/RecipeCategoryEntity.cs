using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes.Entities;

public sealed class RecipeCategoryEntity : Entity
{
    public long RecipeId { get; }

    public int CategoryId { get; }

    public RecipeCategoryEntity(int categoryId)
    {
        CategoryId = categoryId;
    }

    public override object GetPrimaryKey() => new RecipeCategoryPrimaryKey
    {
        RecipeId = RecipeId,
        CategoryId = CategoryId,
    };
}
