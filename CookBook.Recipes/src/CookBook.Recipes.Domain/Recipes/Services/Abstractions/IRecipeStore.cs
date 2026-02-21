namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface IRecipeStore
{
    public ValueTask<RecipeAggregate?> FindByRecipeId(
        long recipeId,
        CancellationToken cancellationToken);

    public Task Add(
        RecipeAggregate recipe,
        CancellationToken cancellationToken);

    public Task Update(
        RecipeAggregate recipe,
        CancellationToken cancellationToken);

    public Task Delete(
        RecipeAggregate recipe,
        CancellationToken cancellationToken);
}
