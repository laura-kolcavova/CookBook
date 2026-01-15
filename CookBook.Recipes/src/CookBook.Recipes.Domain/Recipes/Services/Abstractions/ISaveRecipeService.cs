using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface ISaveRecipeService
{
    public Task<Result<SaveRecipeResult, Error>> SaveRecipe(
        SaveRecipeParams saveRecipeParams,
        CancellationToken cancellationToken);
}
