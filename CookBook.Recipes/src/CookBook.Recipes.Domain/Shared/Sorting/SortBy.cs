namespace CookBook.Recipes.Domain.Shared.Sorting;

public record SortBy
{
    public required string PropertyName { get; init; }

    public required SortingDirection Direction { get; init; }
}
