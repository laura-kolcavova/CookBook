using CookBook.Recipes.Domain.Recipes;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Persistence.Recipes.Extensions;

internal static class RecipeAggregateExtensions
{
    public static async ValueTask<RecipeAggregate?> FetchRecipeAsync(
        this DbSet<RecipeAggregate> recipes,
        long recipeId,
        CancellationToken cancellationToken)
    {
        var recipe = await recipes.FindAsync(
            recipeId,
            cancellationToken);

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
        }

        return recipe;
    }

    public static IQueryable<RecipeAggregate> IncludeAll(
        this IQueryable<RecipeAggregate> queryable)
    {
        return queryable
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Instructions);
    }
}
