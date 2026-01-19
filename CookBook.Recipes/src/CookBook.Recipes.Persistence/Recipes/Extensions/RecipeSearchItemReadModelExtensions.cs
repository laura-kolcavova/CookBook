using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Persistence.Recipes.Extensions;

internal static class RecipeSearchItemReadModelExtensions
{
    public static IQueryable<RecipeSearchItemReadModel> ProjectToRecipeSearchItemReadModel(
       this IQueryable<RecipeAggregate> recipes)
    {
        return recipes.Select(recipe => new RecipeSearchItemReadModel
        {
            RecipeId = recipe.Id,
            Title = recipe.Title,
            CreatedAt = recipe.CreatedAt!.Value
        });
    }
}
