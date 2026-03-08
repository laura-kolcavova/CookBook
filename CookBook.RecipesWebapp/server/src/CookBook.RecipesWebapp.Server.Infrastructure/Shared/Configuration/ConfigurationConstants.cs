namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class Identity
    {
        public const string CookieName = "CookBook.RecipesWebapp.Identity";
    }

    public static class Antiforgery
    {
        public const string HeaderName = "CookBook.RecipesWebapp.Antiforgery";

        public const string CookieName = "CookBook.RecipesWebapp.Antiforgery.Host";
    }

    public static class AuthenticationPolicies
    {
        public const string Cookie = "CookieAuthenticationPolicy";
    }

    public static class ReverseProxy
    {
        public const string ReverseProxySectionName = "ReverseProxyConfiguration";
    }
}
