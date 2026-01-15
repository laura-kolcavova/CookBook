using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface IGetLatestRecipesService
{
    public Task<IReadOnlyCollection<LatestRecipeReadModel>> GetLatestRecipes(
        int count,
        CancellationToken cancellationToken);
}
