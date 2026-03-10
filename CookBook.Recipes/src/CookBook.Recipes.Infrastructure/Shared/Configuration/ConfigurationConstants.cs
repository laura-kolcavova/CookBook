namespace CookBook.Recipes.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class SqlConnectionStrings
    {
        public const string CookBookRecipesSectionName = "CookBookRecipesSql";
    }

    public static class AuthenticationPolicies
    {
        public const string ReadWrite = "ReadWriteAuthenticationPolicy";
    }

    public static class AuthenticationScopes
    {
        public const string CookBookRecipesReadWrite = "CookBook.Recipes.ReadWrite";
    }
}
