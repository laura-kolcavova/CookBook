using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.Recipes.Domain.Categories;

public static partial class Errors
{
    public static class Category
    {
        public static ExpectedError RootCategoryModificationNotAllowed() =>
            ExpectedError.Failure(
                $"{nameof(Category)}.{nameof(RootCategoryModificationNotAllowed)}",
                "Root category modification is not allowed");

        public static ExpectedError NotFound(long categoryId) =>
           ExpectedError.Failure(
               $"{nameof(Category)}.{nameof(NotFound)}",
               $"A category with id {categoryId} was not found.");

        public static ExpectedError AnotherCategoryWithNameAlreadyExists(string cateogryName) =>
            ExpectedError.Failure(
                $"{nameof(Category)}.{nameof(AnotherCategoryWithNameAlreadyExists)}",
                $"An another category with name {cateogryName} already exists.");

        public static ExpectedError ParentCategoryIsNotValid(string parentCategoryName, string categoryName) =>
           ExpectedError.Failure(
               $"{nameof(Category)}.{nameof(ParentCategoryIsNotValid)}",
               $"{parentCategoryName} is not valid parent category for {categoryName}.");
    }
}
