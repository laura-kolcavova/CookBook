using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface IRemoveRecipeService
{
    public Task<UnitResult<Error>> RemoveRecipe(
    long recipeId,
    CancellationToken cancellationToken);
}
