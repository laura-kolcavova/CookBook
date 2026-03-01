namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class Antiforgery
    {
        public const string HeaderName = "COOK-BOOK-RECIPES-WEBAPP-X-XSRF-TOKEN";

        public const string CookieName = "__Host-COOK-BOOK-RECIPES-WEBAPP-X-XSRF-TOKEN";
    }

    public static class Identity
    {
        public const string CookieName = "CookBook.RecipesWebapp.Identity";
    }
}
