namespace CookBook.Recipes.Api.Features.Shared.Dto;

internal record OffsetFilterRequestDto
{
    public required int? Offset { get; init; }

    public required int? Limit { get; init; }
}
