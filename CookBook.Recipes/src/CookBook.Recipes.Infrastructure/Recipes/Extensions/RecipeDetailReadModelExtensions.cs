using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Infrastructure.Recipes.Extensions;

internal static class RecipeDetailReadModelExtensions
{
    public static IQueryable<RecipeDetailReadModel> ProjectToRecipeDetailReadModel(
        this IQueryable<RecipeAggregate> recipes)
    {
        return recipes.Select(recipe => new RecipeDetailReadModel
        {
            RecipeId = recipe.Id,
            UserId = recipe.UserId,
            Title = recipe.Title,
            Description = recipe.Description,
            Servings = recipe.Servings,
            CookTime = recipe.CookTime,
            CreatedAt = recipe.CreatedAt!.Value,
            Notes = recipe.Notes,
            Ingredients = recipe
                .Ingredients
                .OrderBy(recipeIngredient => recipeIngredient.OrderIndex)
                .Select(recipeIngredient =>
                    new RecipeDetailReadModel.IngredientItem
                    {
                        LocalId = recipeIngredient.LocalId,
                        Note = recipeIngredient.Note,
                    })
                .ToList(),
            Instructions = recipe
                .Instructions
                .OrderBy(recipeInstruction => recipeInstruction.OrderIndex)
                .Select(recipeInstruction =>
                    new RecipeDetailReadModel.InstructionItem
                    {
                        LocalId = recipeInstruction.LocalId,
                        Note = recipeInstruction.Note,
                    })
                .ToList(),
            Tags = recipe
                .RecipeTags
                .Select(recipeTag =>
                    recipeTag.Name)
                .ToList()
        });
    }
}
