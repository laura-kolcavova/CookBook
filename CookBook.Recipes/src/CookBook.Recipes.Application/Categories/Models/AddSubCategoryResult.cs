namespace CookBook.Recipes.Application.Categories.Models;

public sealed record AddSubCategoryResult
{
    public required int CategoryId { get; init; }
}
