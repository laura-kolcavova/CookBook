namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Contracts;

internal sealed record SaveRecipeResponseDto
{
    public required long RecipeId { get; init; }
}
