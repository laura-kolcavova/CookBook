using CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Contracts;
using CookBook.Recipes.Application.Recipes.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class SaveRecipeMapper
{
    public static partial SaveRecipeRequestModel ToModel(
        this SaveRecipeRequestDto source);

    public static partial SaveRecipeResponseDto ToResponseDto(
        this SaveRecipeResultModel source);
}
