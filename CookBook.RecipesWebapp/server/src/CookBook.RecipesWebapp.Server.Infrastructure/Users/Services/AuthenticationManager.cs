using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.RecipesWebapp.Server.Domain.Users;
using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using CSharpFunctionalExtensions;
using OpenIddict.Client;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static OpenIddict.Abstractions.OpenIddictExceptions;
using static OpenIddict.Client.OpenIddictClientModels;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.Services;

internal class AuthenticationManager(
    OpenIddictClientService openIddictClientService) :
    IAuthenticationManager
{
    public async Task<Result<AuthenticationResult, Error>> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        try
        {
            var request = new PasswordAuthenticationRequest
            {
                Username = email,
                Password = password,
                Scopes = [
                    Scopes.Profile,
                    Scopes.Email
                ],
            };

            // IdentityToken and IdentityToken is not available when using password flow so UserInfoTokenPrincipal is used instead
            var passwordAuthenticationResult = await openIddictClientService.AuthenticateWithPasswordAsync(
                request);

            var authenticationResult = new AuthenticationResult
            {
                AccessToken = passwordAuthenticationResult.AccessToken,
                UserInfoTokenPrincipal = passwordAuthenticationResult.UserInfoTokenPrincipal
                    ?? throw new InvalidOperationException("User info token principal is not set.")
            };

            return authenticationResult;
        }
        catch (ProtocolException ex)
        {
            if (ex.Error == Errors.InvalidGrant)
            {
                return UserErrors
                    .User
                    .InvalidCredentials();
            }

            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
