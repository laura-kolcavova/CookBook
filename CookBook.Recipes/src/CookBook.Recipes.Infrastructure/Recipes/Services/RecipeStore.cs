using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;

namespace CookBook.Recipes.Infrastructure.Recipes.Services;

internal sealed class RecipeStore(
    RecipesContext recipesContext) :
    IRecipeStore
{
    public async ValueTask<RecipeAggregate?> FindByRecipeId(
        long recipeId,
        CancellationToken cancellationToken)
    {
        var recipe = await recipesContext
            .Recipes
            .FindAsync(
                recipeId,
                cancellationToken);

        if (recipe is not null)
        {
            await recipesContext
                .Recipes
                .Entry(recipe)
                .Collection(recipe => recipe.Ingredients)
                .LoadAsync(cancellationToken);

            await recipesContext
                .Recipes
                .Entry(recipe)
                .Collection(recipe => recipe.Instructions)
                .LoadAsync(cancellationToken);
        }

        return recipe;
    }

    public async Task Add(
        RecipeAggregate recipe,
        CancellationToken cancellationToken)
    {
        await recipesContext
            .Recipes
            .AddAsync(
                recipe,
                cancellationToken);

        await recipesContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task Update(
        RecipeAggregate recipe,
        CancellationToken cancellationToken)
    {
        recipesContext
            .Recipes
            .Update(recipe);

        await recipesContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task Delete(
        RecipeAggregate recipe,
        CancellationToken cancellationToken)
    {
        recipesContext
            .Recipes
            .Remove(recipe);

        await recipesContext
            .SaveChangesAsync(cancellationToken);
    }
}
