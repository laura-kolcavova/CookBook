using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class SearchRecipesMapper
{
    public static partial SearchRecipesResponseDto.RecipeSearchItemDto ToDto(
        this RecipeSearchItemReadModel source);

    public static partial IReadOnlyCollection<SearchRecipesResponseDto.RecipeSearchItemDto> ToDtoCollection(
        this IEnumerable<RecipeSearchItemReadModel> source);
}
