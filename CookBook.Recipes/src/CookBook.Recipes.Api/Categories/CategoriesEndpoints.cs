using CookBook.Recipes.Api.Categories.Features.AddCategory;
using CookBook.Recipes.Api.Categories.Features.GetCategories;
using CookBook.Recipes.Api.Categories.Features.GetCategoryDetail;

namespace CookBook.Recipes.Api.Categories;

internal static class CategoriesEndpoints
{
    public static RouteGroupBuilder AddCategoriesEndpoints(this RouteGroupBuilder group)
    {
        var categoriesGroup = group
            .MapGroup("/categories")
            .WithTags("Categories");

        AddCategoryEndpoint.Configure(categoriesGroup);
        GetCategoriesEndpoint.Configure(categoriesGroup);
        GetCategoryDetailEndpoint.Configure(categoriesGroup);

        return group;
    }
}
