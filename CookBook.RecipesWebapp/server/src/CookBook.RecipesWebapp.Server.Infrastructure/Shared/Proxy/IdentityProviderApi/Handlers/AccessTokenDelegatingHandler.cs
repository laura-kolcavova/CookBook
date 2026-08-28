using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http.Headers;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Handlers;

internal sealed class AccessTokenDelegatingHandler(
    IHttpContextAccessor httpContextAccessor) :
    DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;

        var accessToken = httpContext is not null
            ? await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
            : null;

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
