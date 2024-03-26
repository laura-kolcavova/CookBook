using CookBook.Recipes.Domain.Categories.ReadModels;

namespace CookBook.Recipes.Api.Categories.Features.GetCategoryDetail;

internal sealed record GetCategoryDetailResponseDto
{
    public required CategoryDetailReadModel Category { get; init; }
}
