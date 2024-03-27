namespace CookBook.Recipes.Api.Categories.Features.RenameCategory;

internal sealed class RenameCategoryRequestDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;
}
