using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Categories.Features.GetCategories;

internal sealed class GetCategoriesRequestDto
{
    [FromQuery(Name = "parentCategoryId")]
    public int? ParentCategoryId { get; init; }
}
