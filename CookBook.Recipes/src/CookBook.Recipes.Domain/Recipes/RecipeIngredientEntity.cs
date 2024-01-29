using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Recipes;

public class RecipeIngredientEntity : IEntity<short>, ITrackableEntity
{
    public short Id { get; }

    public long RecipeId { get; }

    public string Note { get; private set; }

    public short OrderIndex { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public RecipeIngredientEntity()
    {
        Note = string.Empty;
    }

    public RecipeIngredientEntity SetNote(string note)
    {
        Note = note;
        return this;
    }

    public RecipeIngredientEntity SetOrderIndex(short orderIndex)
    {
        OrderIndex = orderIndex;
        return this;
    }
}
