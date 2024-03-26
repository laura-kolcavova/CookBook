using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.GetCategories;

internal sealed class GetCategoriesRequestDto
{
    [FromQuery(Name = "categoryId")]
    public int? CategoryId { get; init; }
}
