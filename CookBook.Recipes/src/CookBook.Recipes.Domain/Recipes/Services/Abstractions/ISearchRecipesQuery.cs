using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;

namespace CookBook.Recipes.Domain.Recipes.Services.Abstractions;

public interface ISearchRecipesQuery
{
    public Task<IReadOnlyCollection<RecipeSearchItemReadModel>> Execute(
        string? searchTerm,
        IEnumerable<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken);
}
