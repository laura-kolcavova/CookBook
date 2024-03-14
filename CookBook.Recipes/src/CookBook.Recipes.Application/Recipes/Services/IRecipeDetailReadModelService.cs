using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface IRecipeDetailReadModelService
{
    public Task<RecipeDetailReadModel?> GetRecipeDetailAsync(
        long recipeId, CancellationToken cancellationToken);
}
