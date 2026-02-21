using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;
using CookBook.Recipes.Infrastructure.Recipes.Extensions;
using CookBook.Recipes.Infrastructure.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Infrastructure.Recipes.Services;

internal sealed class SearchRecipesQuery(
    RecipesContext recipesContext) :
    ISearchRecipesQuery
{
    public async Task<IReadOnlyCollection<RecipeSearchItemReadModel>> Execute(
        string? searchTerm,
        IEnumerable<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken)
    {
        var query = recipesContext
            .Recipes
            .AsNoTracking()
            .Include(recipe => recipe.RecipeTags)
            .AsQueryable();

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

            query = titleResults
                .Union(descriptionResults)
                .Union(tagResults);
        }

        if (sorting is not null)
        {
            query = query
                .SortBy(sorting);
        }

        if (offsetFilter is not null)
        {
            query = query
                .Skip(offsetFilter.Offset)
                .Take(offsetFilter.Limit);
        }

        var searchedRecipes = await query
           .ProjectToRecipeSearchItemReadModel()
           .ToListAsync(cancellationToken);

        return searchedRecipes;
    }
}
