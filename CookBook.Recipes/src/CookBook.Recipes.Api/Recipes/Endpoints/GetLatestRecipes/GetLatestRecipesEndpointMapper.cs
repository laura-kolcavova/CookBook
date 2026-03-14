using CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Contracts;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class GetLatestRecipeEndpointMapper
{
    public static partial LatestRecipeDto ToDto(
        this LatestRecipeReadModel source);

    public static partial IReadOnlyCollection<LatestRecipeDto> ToDtoCollection(
        this IEnumerable<LatestRecipeReadModel> source);
}