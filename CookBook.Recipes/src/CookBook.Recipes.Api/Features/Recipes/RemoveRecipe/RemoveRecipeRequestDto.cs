namespace CookBook.Recipes.Api.Features.Recipes.RemoveRecipe;

internal sealed record RemoveRecipeRequestDto
{
    public required long RecipeId { get; init; }
}
