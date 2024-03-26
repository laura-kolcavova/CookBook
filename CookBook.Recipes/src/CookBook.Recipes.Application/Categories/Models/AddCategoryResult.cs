namespace CookBook.Recipes.Application.Categories.Models;

public sealed record AddCategoryResult
{
    public required int CategoryId { get; init; }
}
