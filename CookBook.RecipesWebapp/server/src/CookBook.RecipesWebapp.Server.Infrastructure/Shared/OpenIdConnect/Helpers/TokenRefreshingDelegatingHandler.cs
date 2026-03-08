using OpenIddict.Client;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static OpenIddict.Client.AspNetCore.OpenIddictClientAspNetCoreConstants;
using static OpenIddict.Client.OpenIddictClientModels;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect.Helpers;

internal sealed class TokenRefreshingDelegatingHandler(
    OpenIddictClientService service,
    HttpMessageHandler innerHandler) :
    DelegatingHandler(
        innerHandler)
{
    private readonly OpenIddictClientService _service = service
        ?? throw new ArgumentNullException(nameof(service));

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var date = GetBackchannelAccessTokenExpirationDate(request.Options);

        if (date is null || TimeProvider.System.GetUtcNow() <= date?.AddMinutes(-5))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                Schemes.Bearer,
                GetBackchannelAccessToken(request.Options));

            var response = await base.SendAsync(
                request,
                cancellationToken);

            if (response.StatusCode is not HttpStatusCode.Unauthorized)
            {
                return response;
            }

            var result = await _service.AuthenticateWithRefreshTokenAsync(
                new RefreshTokenAuthenticationRequest
                {
                    CancellationToken = cancellationToken,
                    DisableUserInfo = true,
                    RefreshToken = GetRefreshToken(request.Options)
                });

            request.Headers.Authorization = new AuthenticationHeaderValue(
                Schemes.Bearer,
                result.AccessToken);

            var newResponse = await base.SendAsync(
                    request,
                    cancellationToken);

            return new TokenRefreshingHttpResponseMessage(
                result,
                newResponse);
        }
        else
        {
            var result = await _service.AuthenticateWithRefreshTokenAsync(
                new RefreshTokenAuthenticationRequest
                {
                    CancellationToken = cancellationToken,
                    DisableUserInfo = true,
                    RefreshToken = GetRefreshToken(request.Options)
                });

            request.Headers.Authorization = new AuthenticationHeaderValue(
                Schemes.Bearer,
                result.AccessToken);

            var response = await base.SendAsync(
                request,
                cancellationToken);

            return new TokenRefreshingHttpResponseMessage(
                result,
                response);
        }
    }

    private static string GetBackchannelAccessToken(
        HttpRequestOptions options)
    {
        return options.TryGetValue(
            new(Tokens.BackchannelAccessToken),
            out string? token) &&
            !string.IsNullOrEmpty(token)
            ? token
            : throw new InvalidOperationException("The access token couldn't be found in the request options.");
    }

    private static DateTimeOffset? GetBackchannelAccessTokenExpirationDate(
        HttpRequestOptions options)
    {
        return options.TryGetValue(
            new(Tokens.BackchannelAccessTokenExpirationDate),
            out string? token) &&
            !string.IsNullOrEmpty(token) &&
            DateTimeOffset.TryParse(
                token,
                CultureInfo.InvariantCulture,
                out DateTimeOffset date)
            ? date
            : null;
    }

    private static string GetRefreshToken(
        HttpRequestOptions options)
    {
        return options.TryGetValue(
            new(Tokens.RefreshToken),
            out string? token) &&
            !string.IsNullOrEmpty(token)
            ? token
            : throw new InvalidOperationException("The refresh token couldn't be found in the request options.");
    }

}
