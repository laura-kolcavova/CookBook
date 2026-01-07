namespace CookBook.Recipes.Domain.Recipes.Models;

public record SaveRecipeResult
{
    public required long RecipeId { get; init; }
}
