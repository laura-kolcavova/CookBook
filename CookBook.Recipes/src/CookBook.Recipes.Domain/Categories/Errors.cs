using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.Recipes.Domain.Categories;

public static partial class Errors
{
    public static class Category
    {
        public static Error RootCategoryModificationNotAllowed() =>
            Error.Failure(
                $"{nameof(Category)}.{nameof(RootCategoryModificationNotAllowed)}",
                "Root category modification is not allowed");

        public static Error NotFound(long categoryId) =>
            Error.Failure(
                $"{nameof(Category)}.{nameof(NotFound)}",
                $"A category with id {categoryId} was not found.");

        public static Error AnotherCategoryWithNameAlreadyExists(string categoryName) =>
            Error.Failure(
                $"{nameof(Category)}.{nameof(AnotherCategoryWithNameAlreadyExists)}",
                $"An another category with name {categoryName} already exists.");

        public static Error ParentCategoryIsNotValid(string parentCategoryName, string categoryName) =>
            Error.Failure(
                $"{nameof(Category)}.{nameof(ParentCategoryIsNotValid)}",
                $"{parentCategoryName} is not valid parent category for {categoryName}.");
    }
}
