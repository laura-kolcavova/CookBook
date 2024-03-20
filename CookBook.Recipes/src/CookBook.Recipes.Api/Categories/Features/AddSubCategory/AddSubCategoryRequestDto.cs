namespace CookBook.Recipes.Api.Categories.Features.AddSubCategory;

public sealed class AddSubCategoryRequestDto
{
    public string Name { get; init; } = string.Empty;

    public int ParentCategoryId { get; init; }
}
