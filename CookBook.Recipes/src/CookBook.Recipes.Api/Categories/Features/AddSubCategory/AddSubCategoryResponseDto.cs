namespace CookBook.Recipes.Api.Categories.Features.AddSubCategory;

internal sealed record AddSubCategoryResponseDto
{
    public required int CategoryId { get; init; }
}
