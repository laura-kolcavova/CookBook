namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsApiRequest(this HttpRequest httpRequest)
    {
        return
            httpRequest.Path.HasValue &&
            httpRequest.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsLessKnownRequest(this HttpRequest httpRequest)
    {
        return
            httpRequest.Path.HasValue &&
            httpRequest.Path.StartsWithSegments("/.less-known", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsRenderingRequest(this HttpRequest httpRequest)
    {
        return Array.Exists(
            httpRequest.Headers.GetCommaSeparatedValues("Accept"),
            v => v.StartsWith("text/html", StringComparison.OrdinalIgnoreCase));
    }
}
