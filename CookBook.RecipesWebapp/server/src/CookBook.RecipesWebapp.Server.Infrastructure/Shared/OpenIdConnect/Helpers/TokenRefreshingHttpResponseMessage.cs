using static OpenIddict.Client.OpenIddictClientModels;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect.Helpers;

public sealed class TokenRefreshingHttpResponseMessage :
    HttpResponseMessage
{
    public RefreshTokenAuthenticationResult RefreshTokenAuthenticationResult { get; }

    public TokenRefreshingHttpResponseMessage(
        RefreshTokenAuthenticationResult result,
        HttpResponseMessage response)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(result);

        RefreshTokenAuthenticationResult = result;

        Content = response.Content;
        StatusCode = response.StatusCode;
        Version = response.Version;

        foreach (var header in response.Headers)
        {
            Headers.Add(header.Key, header.Value);
        }
    }
}
