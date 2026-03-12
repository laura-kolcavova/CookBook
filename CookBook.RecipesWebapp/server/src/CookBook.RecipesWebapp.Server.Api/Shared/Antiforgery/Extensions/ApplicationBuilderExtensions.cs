using CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.Middlewares;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAntiforgeryTokens(
        this IApplicationBuilder builder)
    {
        builder.UseMiddleware<AntiforgeryTokensMiddleware>();

        return builder;
    }
}
