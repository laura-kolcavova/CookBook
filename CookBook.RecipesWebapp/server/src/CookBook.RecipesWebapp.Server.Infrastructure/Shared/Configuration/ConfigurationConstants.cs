namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class CorsPolicies
    {
        public const string Main = "CorsPolicy.Main";
    }

    public static class Identity
    {
        public const string CookieName = "CookBook.RecipesWebapp.Identity";
    }

    public static class Antiforgery
    {
        public const string HeaderName = "CookBook.RecipesWebapp.Antiforgery";

        public const string CookieName = "CookBook.RecipesWebapp.Antiforgery.Host";
    }
}
