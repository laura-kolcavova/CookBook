namespace CookBook.Recipes.Domain.Shared.Filtering;

public record OffsetFilter
{
    public required int Limit { get; init; }

    public required int Offset { get; init; }
}
