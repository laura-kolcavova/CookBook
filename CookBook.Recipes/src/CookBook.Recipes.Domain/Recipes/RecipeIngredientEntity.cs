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

    public RecipeIngredientEntity(string note)
    {
        Note = note;
    }

    public RecipeIngredientEntity SetNote(string newNote)
    {
        Note = newNote;
        return this;
    }

    public RecipeIngredientEntity SetOrderIndex(short newOrderIndex)
    {
        OrderIndex = newOrderIndex;
        return this;
    }
}
