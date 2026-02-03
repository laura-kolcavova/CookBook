using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.UseCases.Abstractions;

public interface ISaveRecipeUseCase
{
    public Task<Result<SaveRecipeResult, Error>> SaveRecipe(
        SaveRecipeParams saveRecipeParams,
        CancellationToken cancellationToken);
}
