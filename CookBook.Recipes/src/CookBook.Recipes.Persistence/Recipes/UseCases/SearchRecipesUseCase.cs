using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CookBook.Recipes.Persistence.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.UseCases;

internal sealed class SearchRecipesUseCase(
    RecipesContext recipesContext,
    ILogger<SearchRecipesUseCase> logger) :
    ISearchRecipesUseCase
{
    public async Task<IReadOnlyCollection<RecipeSearchItemReadModel>> SearchRecipes(
        string? searchTerm,
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["SearchTerm"] = searchTerm
        });

        try
        {
            var queryable = recipesContext.Recipes
                .Include(recipe => recipe.RecipeTags)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchPattern = $"%{searchTerm.ToUpper()}%";

                var titleResults = recipesContext.Recipes
                    .AsNoTracking()
                    .Include(recipe => recipe.RecipeTags)
                    .Where(recipe =>
                        EF.Functions.Like(
                            recipe.Title.ToUpper(),
                            searchPattern));

                var descriptionResults = recipesContext.Recipes
                    .AsNoTracking()
                    .Include(recipe => recipe.RecipeTags)
                    .Where(recipe => recipe.Description != null)
                    .Where(recipe =>
                        EF.Functions.Like(
                            recipe.Description!.ToUpper(),
                            searchPattern));

                var tagResults = recipesContext.Recipes
                    .Include(recipe => recipe.RecipeTags)
                    .AsNoTracking()
                    .Where(recipe =>
                        recipe.RecipeTags
                            .Any(tag =>
                                EF.Functions.Like(
                                    tag.Name.ToUpper(),
                                    searchPattern)));

                queryable = titleResults
                    .Union(descriptionResults)
                    .Union(tagResults);
            }

            if (sorting is not null)
            {
                queryable = queryable
                    .SortBy(sorting);
            }

            if (offsetFilter is not null)
            {
                queryable = queryable
                    .Skip(offsetFilter.Offset)
                    .Take(offsetFilter.Limit);
            }

            return await queryable
               .ProjectToRecipeSearchItemReadModel()
               .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while searching for recipes");
        }
    }
}
