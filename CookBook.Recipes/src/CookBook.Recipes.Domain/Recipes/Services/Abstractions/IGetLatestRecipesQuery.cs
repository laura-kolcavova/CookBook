using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface IGetLatestRecipesQuery
{
    public Task<IReadOnlyCollection<LatestRecipeReadModel>> Execute(
        int count,
        CancellationToken cancellationToken);
}
