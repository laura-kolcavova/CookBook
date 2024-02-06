using CookBook.Recipes.Domain.Entities.Recipes;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Persistence.Extensions;

public static class RecipeAggregateQueryableExtensions
{
    public static IQueryable<RecipeAggregate> IncludeAll(this IQueryable<RecipeAggregate> queryable)
    {
        return queryable
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Instructions);
    }
}
