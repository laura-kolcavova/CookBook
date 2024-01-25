using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Recipes;

public class RecipeInstructionEntity : IEntity<short>, ITrackableEntity
{
    public short Id { get; }

    public long RecipeId { get; }

    public string Note { get; private set; }

    public short OrderIndex { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public RecipeInstructionEntity(string note)
    {
        Note = note;
    }

    public RecipeInstructionEntity SetNote(string newNote)
    {
        Note = newNote;
        return this;
    }

    public RecipeInstructionEntity SetOrderIndex(short newOrderIndex)
    {
        OrderIndex = newOrderIndex;
        return this;
    }
}
