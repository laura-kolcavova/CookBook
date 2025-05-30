using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Persistence.Recipes.Extensions;

internal static class RecipeListingItemReadModelExtensions
{
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
}
