using CookBook.Recipes.Api.Categories.Features.AddMainCategory;
using CookBook.Recipes.Api.Categories.Features.AddSubCategory;

namespace CookBook.Recipes.Api.Categories;

internal static class CategoriesEndpoints
{
    public static RouteGroupBuilder AddCategoriesEndpoints(this RouteGroupBuilder group)
    {
        var categoriesGroup = group
            .MapGroup("/categories")
            .WithTags("Categories");

        AddMainCategoryEndpoint.Configure(categoriesGroup);
        AddSubCategoryEndpoint.Configure(categoriesGroup);

        return group;
    }
}
