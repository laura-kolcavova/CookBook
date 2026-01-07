using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Domain.Recipes.Services;

public interface IRemoveRecipeService
{
    public Task<UnitResult<Error>> RemoveRecipe(
    long recipeId,
    CancellationToken cancellationToken);
}
