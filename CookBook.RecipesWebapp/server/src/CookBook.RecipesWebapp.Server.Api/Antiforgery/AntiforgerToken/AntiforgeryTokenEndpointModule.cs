using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Antiforgery.AntiforgeryToken;

public class AntiforgeryTokenEndpointModule :
    AntiforgeryModule
{
    public override void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app
            .MapGet("/token", Handle)
            .WithName("AntiforgeryToken")
            .WithSummary("Gets and sets antiforgery request token to a JavaScript readable cookie")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddClosedRequest()
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.Cookie);
    }

    private static IResult Handle(
        [FromServices] IAntiforgery antiforgery,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var tokens = antiforgery.GetAndStoreTokens(httpContext);

        httpContext
            .Response
            .Cookies
            .Append(
                "MyAntiforgeryToken",
                tokens.RequestToken!,
                new CookieOptions
                {
                    HttpOnly = false,
                    SameSite = SameSiteMode.Strict
                });

        return TypedResults.NoContent();
    }
}
