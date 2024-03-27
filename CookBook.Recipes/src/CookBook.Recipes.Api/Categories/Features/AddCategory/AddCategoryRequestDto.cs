using CookBook.Recipes.Domain.Categories;

namespace CookBook.Recipes.Api.Categories.Features.AddCategory;

internal sealed class AddCategoryRequestDto
{
    public string Name { get; init; } = string.Empty;

    public int ParentCategoryId { get; init; } = CategoryAggregate.RootCategoryId;
}
