using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Persistence.Recipes.Extensions;

public static class RecipeEntityFrameworkExtensions
{
    public static async ValueTask<RecipeAggregate?> FetchRecipeAsync(
        this DbSet<RecipeAggregate> recipes,
        long recipeId,
        CancellationToken cancellationToken)
    {
        var recipe = await recipes
                .FindAsync(recipeId, cancellationToken);

        if (recipe is not null)
        {
            await recipes
                .Entry(recipe)
                .Collection(recipe => recipe.Ingredients)
                .LoadAsync(cancellationToken);

            await recipes
                .Entry(recipe)
                .Collection(recipe => recipe.Instructions)
                .LoadAsync(cancellationToken);

            await recipes
                .Entry(recipe)
                .Collection(recipe => recipe.RecipeCategories)
                .LoadAsync(cancellationToken);
        }

        return recipe;
    }

    public static IQueryable<RecipeAggregate> IncludeAll(this IQueryable<RecipeAggregate> queryable)
    {
        return queryable
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Instructions)
            .Include(recipe => recipe.RecipeCategories);
    }

    public static IQueryable<RecipeListingItemReadModel> ProjectToRecipeListingItemReadModel(
        this IQueryable<RecipeAggregate> recipes)
    {
        return recipes.Select(recipe => new RecipeListingItemReadModel
        {
            Id = recipe.Id,
            Title = recipe.Title,
            CreatedAt = recipe.CreatedAt!.Value
        });
    }

    public static IQueryable<RecipeDetailReadModel> ProjectToRecipeDetailReadModel(
        this IQueryable<RecipeAggregate> recipes)
    {
        return recipes.Select(recipe => new RecipeDetailReadModel
        {
            Id = recipe.Id,
            UserId = recipe.UserId,
            Title = recipe.Title,
            Description = recipe.Description,
            Servings = recipe.Servings,
            PreparationTime = recipe.PreparationTime,
            CookTime = recipe.CookTime,
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
            Categories = recipe
                .RecipeCategories
                .Select(recipeCategory =>
                    new RecipeDetailReadModel.CategoryItem
                    {
                        Id = recipeCategory.CategoryId,
                        Name = string.Empty
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
