using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface IGetRecipeDetailService
{
    public Task<RecipeDetailReadModel?> GetRecipeDetail(
       long recipeId, CancellationToken cancellationToken);
}
