using CookBook.Recipes.Domain.Recipes;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Infrastructure.Recipes.Extensions;

internal static class RecipesQueryableExtensions
{
    public static IQueryable<RecipeAggregate> IncludeAll(
        this IQueryable<RecipeAggregate> queryable)
    {
        return queryable
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Instructions);
    }
}
