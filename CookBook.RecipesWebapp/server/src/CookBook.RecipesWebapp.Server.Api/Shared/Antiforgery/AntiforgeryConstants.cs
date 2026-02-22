namespace CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery;

internal static class AntiforgeryConstants
{
    public const string HeaderName = "X-XSRF-TOKEN";

    public const string CookieName = "__Host-X-XSRF-TOKEN";
}
