using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.Recipes.Domain.Recipes;

public static partial class Errors
{
    public static class Recipe
    {
        public static Error NotFound(long recipeId) =>
          Error.Failure(
              $"{nameof(Recipe)}.{nameof(NotFound)}",
              $"Recipe with id {recipeId} was not found.");
    }
}
