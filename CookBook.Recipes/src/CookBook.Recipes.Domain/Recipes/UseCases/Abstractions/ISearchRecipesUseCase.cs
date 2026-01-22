using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;

namespace CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;

public interface ISearchRecipesUseCase
{
    public Task<IReadOnlyCollection<RecipeSearchItemReadModel>> SearchRecipes(
        string? searchTerm,
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken);
}
