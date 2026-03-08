using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace CookBook.IdentityProvider.Api.Endpoints.Authorization.LogOut;

public class LogOutEndpointModule :
    AuthorizationModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapMethods(
                "/logout",
                [HttpMethods.Get, HttpMethods.Post],
                HandleAsync)
            .WithName("LogOut")
            .WithSummary("OpenID Connect logout endpoint")
            .WithDescription("")
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .HandleOperationCancelled();
    }

    private static async Task<IResult> HandleAsync(
        [FromServices] SignInManager<CustomIdentityUser> signInManager,
        CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return TypedResults.SignOut(
            authenticationSchemes: [
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            ],
            properties: new AuthenticationProperties
            {
                RedirectUri = "/"
            });
    }
}
