namespace CookBook.Recipes.Application.Recipes.Models;

public record SaveRecipeResult
{
    public required long RecipeId { get; init; }
}
