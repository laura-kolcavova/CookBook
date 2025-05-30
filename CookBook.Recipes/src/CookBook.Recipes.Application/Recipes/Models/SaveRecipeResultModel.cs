namespace CookBook.Recipes.Application.Recipes.Models;

public record SaveRecipeResultModel
{
    public required long RecipeId { get; init; }
}
