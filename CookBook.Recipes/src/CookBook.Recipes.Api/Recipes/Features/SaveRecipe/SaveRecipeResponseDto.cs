namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe;

internal sealed record SaveRecipeResponseDto
{
    public required long RecipeId { get; init; }
}
