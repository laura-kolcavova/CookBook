using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Categories.Services;

public interface ICategoryService
{
    public Task<Result<AddMainCategoryResult, ExpectedError>> AddMainCategory(
        string name,
        CancellationToken cancellationToken);

    public Task<Result<AddSubCategoryResult, ExpectedError>> AddSubCategory(
        string name,
        int parentCategoryId,
        CancellationTokenSource cancellationToken);

    public Task<UnitResult<ExpectedError>> MoveSubCategory(
        int id,
        int parentCategoryId,
        CancellationTokenSource cancellationToken);

    public Task<UnitResult<ExpectedError>> RenameCategory(
        int id,
        CancellationToken cancellationToken);

    public Task<UnitResult<ExpectedError>> RemoveCategory(
        int id,
        CancellationToken cancellationToken);
}
