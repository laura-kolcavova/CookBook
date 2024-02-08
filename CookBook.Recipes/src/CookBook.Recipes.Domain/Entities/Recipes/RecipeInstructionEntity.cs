using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Entities.Recipes;

public class RecipeInstructionEntity : Entity<long>, ITrackableEntity
{
    public long RecipeId { get; }

    public string Note { get; private set; } = string.Empty;

    public short OrderIndex { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public RecipeInstructionEntity()
    {
    }

    public RecipeInstructionEntity(long id)
        : base(id)
    {
    }

    public void SetNote(string note)
    {
        Note = note;
    }

    public void SetOrderIndex(short orderIndex)
    {
        OrderIndex = orderIndex;
    }
}
