using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface IGetRecipeDetailQuery
{
    public Task<RecipeDetailReadModel?> Execute(
        long recipeId,
        CancellationToken cancellationToken);
}
