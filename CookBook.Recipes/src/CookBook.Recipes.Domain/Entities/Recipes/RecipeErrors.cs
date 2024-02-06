using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.Recipes.Domain.Entities.Recipes;
public static class RecipeErrors
{
    private const string prefix = "Recipe";

    public static ExpectedError NotFound(long recipeId) => ExpectedError.NotFound(
        $"{prefix}.{nameof(NotFound)}",
        $"Recipe with id {recipeId} was not found.");
}
