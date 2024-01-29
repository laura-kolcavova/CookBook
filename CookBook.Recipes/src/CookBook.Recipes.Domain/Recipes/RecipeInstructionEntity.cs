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

    public RecipeInstructionEntity()
    {
        Note = string.Empty;
    }

    public RecipeInstructionEntity SetNote(string note)
    {
        Note = note;
        return this;
    }

    public RecipeInstructionEntity SetOrderIndex(short orderIndex)
    {
        OrderIndex = orderIndex;
        return this;
    }
}
