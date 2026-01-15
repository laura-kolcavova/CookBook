using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Persistence.Recipes.Extensions;

internal static class LatestRecipeModelExtensions
{
    public static IQueryable<LatestRecipeReadModel> ProjectToLatestRecipeReadModel(
        this IQueryable<RecipeAggregate> recipes)
    {
        return recipes.Select(recipe => new LatestRecipeReadModel
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            CreatedAt = recipe.CreatedAt!.Value,
            ImageUrl = string.Empty
        });
    }
}
