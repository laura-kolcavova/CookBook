namespace CookBook.Recipes.Application.Common.Sorting;

public record SortBy
{
    public required string PropertyName { get; init; }

    public required SortingDirection Direction { get; init; }
}
