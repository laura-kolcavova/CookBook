using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.Recipes.Domain.Recipes;

public static partial class Errors
{
    public static class Recipe
    {
        public static ExpectedError NotFound(long recipeId) =>
          ExpectedError.Validation(
              $"{nameof(Recipe)}.{nameof(NotFound)}",
              $"Recipe with id {recipeId} was not found.");
    }
}
