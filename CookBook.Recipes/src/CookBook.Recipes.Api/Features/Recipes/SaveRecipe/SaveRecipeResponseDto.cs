namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

internal sealed record SaveRecipeResponseDto
{
    public required long RecipeId { get; init; }
}
