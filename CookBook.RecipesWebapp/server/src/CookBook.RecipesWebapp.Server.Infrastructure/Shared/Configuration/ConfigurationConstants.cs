namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class Identity
    {
        public const string CookieName = "CookBook.RecipesWebapp.Identity";
    }

    public static class Antiforgery
    {
        public const string TokenCookieName = "CookBook.RecipesWebapp.Antiforgery.CookieToken";

        public const string RequestTokenCookieName = "CookBook.RecipesWebapp.Antiforgery.RequestToken";

        public const string RequestVerificationTokenHeaderName = "CookBook.RecipesWebapp.Antiforgery.RequestVerificationToken";
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
