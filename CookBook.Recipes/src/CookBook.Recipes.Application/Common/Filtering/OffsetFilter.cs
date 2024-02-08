namespace CookBook.Recipes.Application.Common.Filtering;

public record OffsetFilter
{
    public required int Limit { get; init; }

    public required int Offset { get; init; }
}
