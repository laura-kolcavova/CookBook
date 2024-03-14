using CookBook.Recipes.Api.Recipes.Features.SaveRecipe;
using CookBook.Recipes.Application.Recipes.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

[Mapper]
internal static partial class SaveRecipeMapper
{
    public static partial SaveRecipeRequest ToSaveRecipeRequest(SaveRecipeRequestDto request);

    public static SaveRecipeResponseDto ToResponse(SaveRecipeResult saveRecipeResult)
    {
        return new SaveRecipeResponseDto
        {
            RecipeId = saveRecipeResult.RecipeId
        };
    }
}
