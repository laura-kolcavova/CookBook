using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class SearchRecipesEndpointMapper
{
    public static partial RecipeSearchItemDto ToDto(
        this RecipeSearchItemReadModel source);

    public static partial IReadOnlyCollection<RecipeSearchItemDto> ToDtoCollection(
        this IEnumerable<RecipeSearchItemReadModel> source);
}
