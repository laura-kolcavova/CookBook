using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using OpenIddict.Client;
using static OpenIddict.Client.OpenIddictClientModels;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.Services;

internal class AuthenticationManager(
    OpenIddictClientService openIddictClientService) :
    IAuthenticationManager
{
    public async Task<AuthenticationResult> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var request = new PasswordAuthenticationRequest
        {
            Username = email,
            Password = password
        };

        var passwordAuthenticationResult = await openIddictClientService.AuthenticateWithPasswordAsync(
            request);

        var authenticationResult = new AuthenticationResult
        {
            AccessToken = passwordAuthenticationResult.AccessToken
        };

        return authenticationResult;
    }
}
