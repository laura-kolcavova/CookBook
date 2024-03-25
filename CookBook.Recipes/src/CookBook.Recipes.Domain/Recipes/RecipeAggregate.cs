using CookBook.Recipes.Domain.Recipes.Parameters;
using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes;

public sealed class RecipeAggregate : AggregateRoot<long>, ITrackableEntity
{
    public long Id { get; }

    public int UserId { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public string? Notes { get; private set; }

    public short Servings { get; private set; }

    public short PreparationTime { get; private set; }

    public short CookTime { get; private set; }

    public DateTimeOffset? CreatedAt { get; }

    public DateTimeOffset? UpdatedAt { get; }

    #region NavigationProperties

    private readonly List<RecipeIngredientEntity> _ingredients;

    private readonly List<RecipeInstructionEntity> _instructions;

    public IReadOnlyCollection<RecipeIngredientEntity> Ingredients => _ingredients;

    public IReadOnlyCollection<RecipeInstructionEntity> Instructions => _instructions;

    #endregion NavigationProperties

    public RecipeAggregate(string title, int userId)
    {
        Title = title;
        UserId = userId;

        _ingredients = new List<RecipeIngredientEntity>();
        _instructions = new List<RecipeInstructionEntity>();
    }

    public override long GetPrimaryKey() => Id;

    public void SetTitle(string title)
    {
        Title = title;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetNotes(string? notes)
    {
        Notes = notes;
    }

    public void SetServings(short servings)
    {
        Servings = servings;
    }

    public void SetPreparationTime(short preparationTime)
    {
        PreparationTime = preparationTime;
    }

    public void SetCookTime(short cookTime)
    {
        CookTime = cookTime;
    }

    public void SaveIngredients(SaveIngredientsParameters saveIngredientsParameters)
    {
        var newIngredients = new List<RecipeIngredientEntity>();
        var lastLocalId = _ingredients.LastOrDefault()?.LocalId ?? 0;
        short orderIndex = 10;

        foreach (var ingredientParameters in saveIngredientsParameters.Ingredients)
        {
            var ingredient = CreateOrUpdateIngredient(
                ingredientParameters, orderIndex, ref lastLocalId);

            newIngredients.Add(ingredient);
            orderIndex += 10;
        }

        _ingredients.Clear();
        _ingredients.AddRange(newIngredients);
    }

    public void SaveInstructions(SaveInstructionsParameters saveInstructionsParameters)
    {
        var newInstructions = new List<RecipeInstructionEntity>();
        var lastLocalId = _instructions.LastOrDefault()?.LocalId ?? 0;
        short orderIndex = 10;

        foreach (var instructionParameters in saveInstructionsParameters.Instructions)
        {
            var instruction = CreateOrUpdateInstruction(
                instructionParameters, orderIndex, ref lastLocalId);

            newInstructions.Add(instruction);
            orderIndex += 10;
        }

        _instructions.Clear();
        _instructions.AddRange(newInstructions);
    }

    private RecipeIngredientEntity CreateOrUpdateIngredient(
        SaveIngredientsParameters.IngredientParameters ingredientParameters,
        short orderIndex,
        ref int lastLocalId)
    {
        var ingredient = ingredientParameters.LocalId is null || ingredientParameters.LocalId <= 0
            ? null
            : _ingredients.FirstOrDefault(ingredient => ingredient.LocalId == ingredientParameters.LocalId);

        if (ingredient is null)
        {
            lastLocalId++;
            ingredient = new RecipeIngredientEntity(lastLocalId, ingredientParameters.Note);
        }
        else
        {
            ingredient.SetNote(ingredientParameters.Note);
        }

        ingredient.SetOrderIndex(orderIndex);

        return ingredient;
    }

    private RecipeInstructionEntity CreateOrUpdateInstruction(
         SaveInstructionsParameters.InstructionParameters instructionParameters,
         short orderIndex,
         ref int lastLocalId)
    {
        var instruction = instructionParameters.LocalId is null || instructionParameters.LocalId <= 0
            ? null
            : _instructions.FirstOrDefault(instruction => instruction.LocalId == instructionParameters.LocalId);

        if (instruction is null)
        {
            lastLocalId++;
            instruction = new RecipeInstructionEntity(lastLocalId, instructionParameters.Note);
        }

        instruction.SetOrderIndex(orderIndex);

        return instruction;
    }
}
