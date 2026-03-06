namespace CookBook.IdentityProvider.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class SqlConnectionStrings
    {
        public const string CookBookUsersSectionName = "CookBookUsersSql";
    }

    public static class IdentityApplication
    {
        public const string CookieName = "CookBook.IdentityProvider.Identity";
    }

    public static class Antiforgery
    {
        public const string CookieName = "CookBook.IdentityProvider.Antiforgery.Host";
    }
}
