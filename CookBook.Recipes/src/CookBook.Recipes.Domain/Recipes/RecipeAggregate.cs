using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Domain.Recipes.Models;
using CookBook.Recipes.Domain.Shared.Entities;

namespace CookBook.Recipes.Domain.Recipes;

public sealed class RecipeAggregate :
    AggregateRoot,
    ITrackableEntity
{
    public long Id { get; }

    public string UserName { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public short Servings { get; private set; }

    public short CookTime { get; private set; }

    public string? Notes { get; private set; }

    public DateTimeOffset? CreatedAt { get; }

    public DateTimeOffset? UpdatedAt { get; }

    #region NavigationProperties

    private readonly List<RecipeIngredientEntity> _ingredients;

    private readonly List<RecipeInstructionEntity> _instructions;

    private readonly List<RecipeTagEntity> _recipeTags;

    public IReadOnlyCollection<RecipeIngredientEntity> Ingredients => _ingredients.AsReadOnly();

    public IReadOnlyCollection<RecipeInstructionEntity> Instructions => _instructions.AsReadOnly();

    public IReadOnlyCollection<RecipeTagEntity> RecipeTags => _recipeTags.AsReadOnly();

    #endregion NavigationProperties

    public RecipeAggregate(
        string title,
        string userName)
    {
        Title = title;
        UserName = userName;

        _ingredients = [];
        _instructions = [];
        _recipeTags = [];
    }

    public override object GetPrimaryKey() => Id;

    public void SetTitle(
        string title)
    {
        Title = title;
    }

    public void SetDescription(
        string? description)
    {
        Description = description;
    }

    public void SetNotes(
        string? notes)
    {
        Notes = notes;
    }

    public void SetServings(
        short servings)
    {
        Servings = servings;
    }

    public void SetCookTime(
        short cookTime)
    {
        CookTime = cookTime;
    }

    public void SaveIngredients(
        IEnumerable<SaveIngredientItemParams> saveIngredientItems)
    {
        var recipeIngredients = new List<RecipeIngredientEntity>();

        var lastLocalId = _ingredients.LastOrDefault()?.LocalId ?? 0;

        short orderIndex = 10;

        foreach (var ingredientParameters in saveIngredientItems)
        {
            var ingredient = CreateOrUpdateIngredient(
                ingredientParameters,
                orderIndex,
                ref lastLocalId);

            recipeIngredients.Add(ingredient);

            orderIndex += 10;
        }

        _ingredients.Clear();
        _ingredients.AddRange(recipeIngredients);
    }

    public void SaveInstructions(
        IEnumerable<SaveInstructionItemParams> saveInstructionItems)
    {
        var recipeInstructions = new List<RecipeInstructionEntity>();

        var lastLocalId = _instructions.LastOrDefault()?.LocalId ?? 0;

        short orderIndex = 10;

        foreach (var instructionParameters in saveInstructionItems)
        {
            var instruction = CreateOrUpdateInstruction(
                instructionParameters,
                orderIndex,
                ref lastLocalId);

            recipeInstructions.Add(instruction);

            orderIndex += 10;
        }

        _instructions.Clear();
        _instructions.AddRange(recipeInstructions);
    }

    public void SaveTags(
        IEnumerable<string> tags)
    {
        var recipeTags = tags
            .Select(tagName => new RecipeTagEntity(tagName));

        _recipeTags.Clear();
        _recipeTags.AddRange(recipeTags);
    }

    private RecipeIngredientEntity CreateOrUpdateIngredient(
        SaveIngredientItemParams saveIngredientItem,
        short orderIndex,
        ref int lastLocalId)
    {
        var ingredient =
            saveIngredientItem.LocalId is null ||
            saveIngredientItem.LocalId <= 0
            ? null
            : _ingredients.FirstOrDefault(
                ingredient => ingredient.LocalId == saveIngredientItem.LocalId);

        if (ingredient is null)
        {
            lastLocalId++;

            ingredient = new RecipeIngredientEntity(
                lastLocalId,
                saveIngredientItem.Note);
        }
        else
        {
            ingredient.SetNote(saveIngredientItem.Note);
        }

        ingredient.SetOrderIndex(orderIndex);

        return ingredient;
    }

    private RecipeInstructionEntity CreateOrUpdateInstruction(
         SaveInstructionItemParams saveInstructionItem,
         short orderIndex,
         ref int lastLocalId)
    {
        var instruction =
            saveInstructionItem.LocalId is null ||
            saveInstructionItem.LocalId <= 0
            ? null
            : _instructions.FirstOrDefault(
                instruction => instruction.LocalId == saveInstructionItem.LocalId);

        if (instruction is null)
        {
            lastLocalId++;

            instruction = new RecipeInstructionEntity(
                lastLocalId,
                saveInstructionItem.Note);
        }
        else
        {
            instruction.SetNote(saveInstructionItem.Note);
        }

        instruction.SetOrderIndex(orderIndex);

        return instruction;
    }
}
