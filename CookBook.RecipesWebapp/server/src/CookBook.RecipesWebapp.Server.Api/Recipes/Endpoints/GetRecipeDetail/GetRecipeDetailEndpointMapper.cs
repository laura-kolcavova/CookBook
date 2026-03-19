using CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.RecipesWebapp.Server.Domain.Recipes.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class GetRecipeDetailEndpointMapper
{
    public static partial RecipeDetailDto ToDto(
        this RecipeDetailModel source);
}
