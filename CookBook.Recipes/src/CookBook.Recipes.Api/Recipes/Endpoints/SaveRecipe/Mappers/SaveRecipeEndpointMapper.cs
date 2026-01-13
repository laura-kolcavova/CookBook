using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;
using CookBook.Recipes.Domain.Recipes.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Mappers;

[Mapper(
    EnumMappingStrategy = EnumMappingStrategy.ByName,
    EnumMappingIgnoreCase = true)]
internal static partial class SaveRecipeEndpointMapper
{
    public static partial SaveRecipeParams ToSaveRecipeParams(
        this SaveRecipeRequestDto source);

    public static partial SaveRecipeResponseDto ToDto(
        this SaveRecipeResult source);
}
