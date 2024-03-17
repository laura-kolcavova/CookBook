namespace CookBook.Recipes.Application.Categories.Models;

public sealed record AddMainCategoryResult
{
    public required int CategoryId { get; init; }
}
