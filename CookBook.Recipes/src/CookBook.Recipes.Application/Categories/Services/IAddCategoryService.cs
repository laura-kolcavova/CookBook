using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Categories.Services;

public interface IAddCategoryService
{
    public Task<Result<AddCategoryResult, Error>> AddCategory(
        string name,
        int parentCategoryId,
        CancellationToken cancellationToken);

    public Task<Result<AddCategoryResult, Error>> AddCategory(
       string name,
       CancellationToken cancellationToken);
}
