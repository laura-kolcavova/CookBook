using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Domain.Recipes;
using MediatR;

namespace CookBook.Recipes.Application.Features.Recipes.SearchRecipes;

public record SearchRecipesQuery : IRequest<IReadOnlyCollection<RecipeListingItemReadModel>>
{
    public required OffsetFilter? OffsetFilter { get; init; }
}
