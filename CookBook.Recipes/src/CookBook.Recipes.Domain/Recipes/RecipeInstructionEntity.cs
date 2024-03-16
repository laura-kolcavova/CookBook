using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes;

public sealed class RecipeInstructionEntity : Entity<RecipeInstructionPrimaryKey>
{
    public long RecipeId { get; }

    public int LocalId { get; }

    public string Note { get; private set; }

    public short OrderIndex { get; private set; }

    public RecipeInstructionEntity(int localId, string note)
    {
        LocalId = localId;
        Note = note;
    }

    public override RecipeInstructionPrimaryKey GetPrimaryKey() => new()
    {
        RecipeId = RecipeId,
        LocalId = LocalId,
    };

    public void SetNote(string note)
    {
        Note = note;
    }

    public void SetOrderIndex(short orderIndex)
    {
        OrderIndex = orderIndex;
    }
}
