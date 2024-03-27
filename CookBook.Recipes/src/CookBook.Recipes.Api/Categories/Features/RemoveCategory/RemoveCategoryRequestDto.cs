using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.RemoveCategory;

internal sealed class RemoveCategoryRequestDto
{
    [FromRoute]
    public int CategoryId { get; set; }
}
