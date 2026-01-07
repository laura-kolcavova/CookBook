namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;

internal sealed record SaveRecipeResponseDto
{
    public required long RecipeId { get; init; }
}
