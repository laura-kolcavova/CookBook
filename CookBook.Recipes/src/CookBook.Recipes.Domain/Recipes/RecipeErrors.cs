using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.Recipes.Domain.Recipes;

public static class RecipeErrors
{
    public static class Recipe
    {
        public static Error NotFound(
            long recipeId) => Error.Failure(
                $"{nameof(Recipe)}.{nameof(NotFound)}",
                $"Recipe with id {recipeId} was not found.");

        public static Error NotOwnedByUser(
            long recipeId,
            int userId) => Error.Failure(
                $"{nameof(Recipe)}.{nameof(NotOwnedByUser)}",
                $"Recipe with id {recipeId} is not owned by user {userId}.");
    }
}
