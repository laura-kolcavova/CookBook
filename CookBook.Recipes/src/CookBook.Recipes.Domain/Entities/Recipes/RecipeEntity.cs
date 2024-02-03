using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Entities.Recipes;

public class RecipeEntity : IEntity<long>, ITrackableEntity
{
    public long Id { get; }

    public int UserId { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public string? Notes { get; private set; }

    public short Servings { get; private set; }

    public short PreparationTime { get; private set; }

    public short CookTime { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    #region NavigationProperties

    private readonly List<RecipeIngredientEntity> _ingredients;

    private readonly List<RecipeInstructionEntity> _instructions;

    public IReadOnlyCollection<RecipeIngredientEntity> Ingredients => _ingredients;

    public IReadOnlyCollection<RecipeInstructionEntity> Instructions => _instructions;

    #endregion NavigationProperties

    public RecipeEntity(int userId)
    {
        UserId = userId;

        Title = string.Empty;

        _ingredients = new List<RecipeIngredientEntity>();
        _instructions = new List<RecipeInstructionEntity>();
    }

    public RecipeEntity SetTitle(string title)
    {
        Title = title;
        return this;
    }

    public RecipeEntity SetDescription(string? description)
    {
        Description = description;
        return this;
    }

    public RecipeEntity SetNotes(string? notes)
    {
        Notes = notes;
        return this;
    }

    public RecipeEntity SetServings(short servings)
    {
        Servings = servings;
        return this;
    }

    public RecipeEntity SetPreparationTime(short preparationTime)
    {
        PreparationTime = preparationTime;
        return this;
    }

    public RecipeEntity SetCookTime(short cookTime)
    {
        CookTime = cookTime;
        return this;
    }

    public RecipeEntity AddIngredient(string note, short orderIndex = 0)
    {
        var newIngredient = new RecipeIngredientEntity();

        newIngredient
            .SetNote(note)
            .SetOrderIndex(orderIndex);

        _ingredients.Add(newIngredient);

        return this;
    }

    public RecipeEntity RemoveIngredient(short ingredientId)
    {
        var index = _ingredients
            .FindIndex(ingredient => ingredient.Id == ingredientId);

        if (index >= 0)
        {
            _ingredients.RemoveAt(index);
        }

        return this;
    }

    public RecipeEntity RemoveAllIngredients()
    {
        _ingredients.Clear();
        return this;
    }

    public RecipeEntity AddInstruction(string note, short orderIndex = 0)
    {
        var newInstruction = new RecipeInstructionEntity();

        newInstruction
            .SetNote(note)
            .SetOrderIndex(orderIndex);

        _instructions.Add(newInstruction);

        return this;
    }

    public RecipeEntity RemoveInstruction(short instructionId)
    {
        var index = _instructions
            .FindIndex(instruction => instruction.Id == instructionId);

        if (index >= 0)
        {
            _instructions.RemoveAt(index);
        }

        return this;
    }

    public RecipeEntity RemoveAllInstructions()
    {
        _instructions.Clear();
        return this;
    }
}
