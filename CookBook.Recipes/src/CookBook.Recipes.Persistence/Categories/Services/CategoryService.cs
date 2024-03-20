using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        RecipesContext recipesContext,
        ILogger<CategoryService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<Result<AddMainCategoryResult, ExpectedError>> AddMainCategory(
        string name,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["Name"] = name,
        }))
        {
            try
            {
                var nameExists = await _recipesContext.Categories
                    .AnyAsync(category =>
                        category.Name == name &&
                        category.ParentCategoryId == CategoryAggregate.RootCategoryId,
                        cancellationToken);

                if (nameExists)
                {
                    return Result.Failure<AddMainCategoryResult, ExpectedError>(
                        Errors.Category.AnotherCategoryWithNameAlreadyExists(name));
                }

                var newMainCategory = new CategoryAggregate(name);

                await _recipesContext.AddAsync(newMainCategory, cancellationToken);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return new AddMainCategoryResult
                {
                    CategoryId = newMainCategory.Id,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error has occurred while adding a new main category");
                throw;
            }
        }
    }

    public async Task<Result<AddSubCategoryResult, ExpectedError>> AddSubCategory(
        string name,
        int parentCategoryId,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["Name"] = name,
            ["ParentCategoryId"] = parentCategoryId,
        }))
        {
            try
            {
                var mainCategory = await _recipesContext.Categories
                    .FindAsync(parentCategoryId, cancellationToken);

                if (mainCategory is null)
                {
                    return Result.Failure<AddSubCategoryResult, ExpectedError>(
                        Errors.Category.NotFound(parentCategoryId));
                }

                var nameExists = await _recipesContext.Categories
                    .AnyAsync(category =>
                       category.Name == name &&
                       category.ParentCategoryId == parentCategoryId,
                       cancellationToken);

                if (nameExists)
                {
                    return Result.Failure<AddSubCategoryResult, ExpectedError>(
                        Errors.Category.AnotherCategoryWithNameAlreadyExists(name));
                }

                var newSubCategory = new CategoryAggregate(name, mainCategory);

                await _recipesContext.AddAsync(newSubCategory, cancellationToken);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return new AddSubCategoryResult
                {
                    CategoryId = newSubCategory.Id,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error has occurred while adding a new sub category");
                throw;
            }
        }
    }

    public async Task<UnitResult<ExpectedError>> MoveSubCategory(
        int id,
        int newParentCategoryId,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["Id"] = id,
            ["NewParentCategoryId"] = newParentCategoryId,
        }))
        {
            try
            {
                var subCategory = await _recipesContext.Categories
                    .FindAsync(id, cancellationToken);

                if (subCategory is null)
                {
                    return Result.Failure<AddSubCategoryResult, ExpectedError>(
                        Errors.Category.NotFound(id));
                }

                var mainCategory = await _recipesContext.Categories
                    .FindAsync(newParentCategoryId, cancellationToken);

                if (mainCategory is null)
                {
                    return Result.Failure<AddSubCategoryResult, ExpectedError>(
                        Errors.Category.NotFound(newParentCategoryId));
                }

                if (mainCategory.Id == subCategory.Id)
                {
                    return Result.Failure<AddSubCategoryResult, ExpectedError>(
                       Errors.Category.ParentCategoryIsNotValid(mainCategory.Name, subCategory.Name));
                }

                subCategory.ChangeParentCategory(mainCategory);

                _recipesContext.Categories.Update(subCategory);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<ExpectedError>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error has occurred while moving a sub category to another parent category");
                throw;
            }
        }
    }

    public async Task<UnitResult<ExpectedError>> RemoveCategory(
        int id,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["Id"] = id,
        }))
        {
            try
            {
                var category = await _recipesContext.Categories
                    .FindAsync(id, cancellationToken);

                if (category is null)
                {
                    return UnitResult.Failure<ExpectedError>(
                        Errors.Category.NotFound(id));
                }

                _recipesContext.Categories.Remove(category);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<ExpectedError>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error has occurred while removing a category");
                throw;
            }
        }
    }

    public async Task<UnitResult<ExpectedError>> RenameCategory(
        int id,
        string newName,
        CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(new Dictionary<string, object?>
        {
            ["Id"] = id,
            ["NewName"] = newName
        }))
        {
            try
            {
                var category = await _recipesContext.Categories
                    .FindAsync(id, cancellationToken);

                if (category is null)
                {
                    return UnitResult.Failure<ExpectedError>(
                        Errors.Category.NotFound(id));
                }

                category.Rename(newName);

                _recipesContext.Categories.Update(category);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<ExpectedError>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error has occurred while renaming a category");
                throw;
            }
        }
    }
}
