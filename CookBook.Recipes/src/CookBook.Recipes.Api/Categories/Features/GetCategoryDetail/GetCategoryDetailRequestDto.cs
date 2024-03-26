using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.GetCategoryDetail;

internal sealed class GetCategoryDetailRequestDto
{
    [FromRoute]
    public int CategoryId { get; init; }
}
