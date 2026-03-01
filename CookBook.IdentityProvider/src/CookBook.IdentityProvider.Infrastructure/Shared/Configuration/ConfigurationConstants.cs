namespace CookBook.IdentityProvider.Infrastructure.Shared.Configuration;

public static class ConfigurationConstants
{
    public static class SqlConnectionStrings
    {
        public const string CookBookUsersSectionName = "CookBookUsersSql";
    }

    public static class CorsPolicies
    {
        public const string Main = "CorsPolicy.Main";
    }

    public static class IdentityApplication
    {
        public const string CookieName = "CookBook.IdentityProvider.Identity";
    }
}
