using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Categories.Services;

public interface ICategoryCommandService
{
    public Task<Result<AddCategoryResult, Error>> AddCategoryAsync(
        string name,
        int parentCategoryId,
        CancellationToken cancellationToken);

    public Task<Result<AddCategoryResult, Error>> AddCategoryAsync(
       string name,
       CancellationToken cancellationToken);

    public Task<UnitResult<Error>> MoveCategoryAsync(
        int id,
        int newParentCategoryId,
        CancellationToken cancellationToken);

    public Task<UnitResult<Error>> RenameCategoryAsync(
        int id,
        string newName,
        CancellationToken cancellationToken);

    public Task<UnitResult<Error>> RemoveCategoryAsync(
        int id,
        CancellationToken cancellationToken);
}
