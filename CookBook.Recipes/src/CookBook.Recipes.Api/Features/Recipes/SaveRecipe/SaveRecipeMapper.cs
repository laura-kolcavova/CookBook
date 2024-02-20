using CookBook.Recipes.Application.Features.Recipes.SaveRecipe;
using Riok.Mapperly.Abstractions;

namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

[Mapper]
internal static partial class SaveRecipeMapper
{
    public static partial SaveRecipeCommand ToCommand(SaveRecipeRequestDto request);

    public static SaveRecipeResponseDto ToResponse(long recipeId)
    {
        return new SaveRecipeResponseDto
        {
            RecipeId = recipeId
        };
    }
}
