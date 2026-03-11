using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Antiforgery;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.Middlewares;

public class AntiforgeryTokensMiddleware
{
    private readonly RequestDelegate _next;

    public AntiforgeryTokensMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        var antiforgery = context
            .RequestServices
            .GetRequiredService<IAntiforgery>();

        var tokens = antiforgery.GetAndStoreTokens(context);

        context
            .Response
            .Cookies
            .Append(
                ConfigurationConstants.Antiforgery.RequestTokenCookieName,
                tokens.RequestToken!,
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    SameSite = SameSiteMode.Strict
                });

        await _next(context);
    }
}
