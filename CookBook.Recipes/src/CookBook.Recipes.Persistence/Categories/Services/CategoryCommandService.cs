using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class CategoryCommandService : ICategoryCommandService
{
    private readonly RecipesContext _recipesContext;
    private readonly ILogger<CategoryCommandService> _logger;

    public CategoryCommandService(
        RecipesContext recipesContext,
        ILogger<CategoryCommandService> logger)
    {
        _recipesContext = recipesContext;
        _logger = logger;
    }

    public async Task<Result<AddCategoryResult, Error>> AddCategoryAsync(
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
                var parentCategory = await _recipesContext.Categories
                        .FindAsync(parentCategoryId, cancellationToken);

                if (parentCategory is null)
                {
                    return Errors.Category.NotFound(parentCategoryId);
                }

                var nameExists = await _recipesContext.Categories
                    .AnyAsync(category =>
                       category.Name == name &&
                       category.ParentCategoryId == parentCategoryId,
                       cancellationToken);

                if (nameExists)
                {
                    return Errors.Category.AnotherCategoryWithNameAlreadyExists(name);
                }

                var newSubCategory = parentCategory is null
                    ? new CategoryAggregate(name)
                    : new CategoryAggregate(name, parentCategory);

                await _recipesContext.AddAsync(newSubCategory, cancellationToken);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return new AddCategoryResult
                {
                    CategoryId = newSubCategory.Id,
                };
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    _logger,
                    ex,
                    "An unexpected error has occurred while adding a new sub category");
            }
        }
    }

    public Task<Result<AddCategoryResult, Error>> AddCategoryAsync(
        string name,
        CancellationToken cancellationToken)
    {
        return AddCategoryAsync(
            name,
            CategoryAggregate.RootCategoryId,
            cancellationToken);
    }

    public async Task<UnitResult<Error>> MoveCategoryAsync(
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
                if (id == CategoryAggregate.RootCategoryId)
                {
                    return Errors.Category.RootCategoryModificationNotAllowed();
                }

                var category = await _recipesContext.Categories
                    .FindAsync(id, cancellationToken);

                if (category is null)
                {
                    return Errors.Category.NotFound(id);
                }

                var parentCategory = await _recipesContext.Categories
                    .FindAsync(newParentCategoryId, cancellationToken);

                if (parentCategory is null)
                {
                    return Errors.Category.NotFound(newParentCategoryId);
                }

                if (category.Id == parentCategory.Id)
                {
                    return Errors.Category.ParentCategoryIsNotValid(
                        parentCategory.Name,
                        category.Name);
                }

                category.ChangeParentCategory(parentCategory);

                _recipesContext.Categories.Update(category);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    _logger,
                    ex,
                    "An unexpected error has occurred while moving a sub category to another parent category");
            }
        }
    }

    public async Task<UnitResult<Error>> RemoveCategoryAsync(
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
                if (id == CategoryAggregate.RootCategoryId)
                {
                    return Errors.Category.RootCategoryModificationNotAllowed();
                }

                var category = await _recipesContext.Categories
                    .FindAsync(id, cancellationToken);

                if (category is null)
                {
                    return Errors.Category.NotFound(id);
                }

                await _recipesContext.Categories
                    .Where(category => category.ParentCategoryId == id)
                    .ExecuteDeleteAsync(cancellationToken);

                _recipesContext.Categories.Remove(category);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    _logger,
                    ex,
                    "An unexpected error has occurred while removing a category");
            }
        }
    }

    public async Task<UnitResult<Error>> RenameCategoryAsync(
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
                if (id == CategoryAggregate.RootCategoryId)
                {
                    return Errors.Category.RootCategoryModificationNotAllowed();
                }

                var category = await _recipesContext.Categories
                    .FindAsync(id, cancellationToken);

                if (category is null)
                {
                    return Errors.Category.NotFound(id);
                }

                category.Rename(newName);

                _recipesContext.Categories.Update(category);

                await _recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    _logger,
                    ex,
                    "An unexpected error has occurred while renaming a category");
            }
        }
    }
}
