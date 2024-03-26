using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Api.Categories.Features.GetCategories;

internal sealed class GetCategoriesResponseDto
{
    public required IReadOnlyCollection<CategoryListingItemReadModel> Categories { get; init; }
}
