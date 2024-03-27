namespace CookBook.Recipes.Api.Categories.Features.MoveCategory;

internal sealed class MoveCategoryRequestDto
{
    public int CategoryId { get; set; }

    public int NewParentCategoryId { get; set; }
}
