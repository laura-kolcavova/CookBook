using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Recipes.UseCases;

internal sealed class SearchRecipesUseCase(
    ISearchRecipesQuery searchRecipesQuery,
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
            var searchedRecipes = await searchRecipesQuery.Execute(
                searchTerm,
                sorting,
                offsetFilter,
                cancellationToken);

            return searchedRecipes;
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while searching for recipes");

            throw;
        }
    }
}
