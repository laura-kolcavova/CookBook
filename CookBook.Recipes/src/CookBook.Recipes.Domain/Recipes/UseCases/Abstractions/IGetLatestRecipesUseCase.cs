using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;

public interface IGetLatestRecipesUseCase
{
    public Task<IReadOnlyCollection<LatestRecipeReadModel>> GetLatestRecipes(
        int count,
        CancellationToken cancellationToken);
}
