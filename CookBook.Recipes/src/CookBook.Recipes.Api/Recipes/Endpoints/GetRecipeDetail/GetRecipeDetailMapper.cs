using CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class GetRecipeDetailMapper
{
    public static partial GetRecipeDetailResponseDto.RecipeDetailDto ToDto(
        this RecipeDetailReadModel source);
}
