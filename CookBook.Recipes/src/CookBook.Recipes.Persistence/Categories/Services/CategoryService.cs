using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CookBook.Recipes.Application.Categories.Services;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class CategoryService : ICategoryService
{
    public Task<Result<AddMainCategoryResult, ExpectedError>> AddMainCategory(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AddSubCategoryResult, ExpectedError>> AddSubCategory(string name, int parentCategoryId, CancellationTokenSource cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UnitResult<ExpectedError>> MoveSubCategory(int id, int parentCategoryId, CancellationTokenSource cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UnitResult<ExpectedError>> RemoveCategory(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UnitResult<ExpectedError>> RenameCategory(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
