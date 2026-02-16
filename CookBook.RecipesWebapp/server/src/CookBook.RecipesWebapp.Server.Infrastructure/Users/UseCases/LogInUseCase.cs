using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using Microsoft.Extensions.Logging;
using OpenIddict.Client;
using static OpenIddict.Client.OpenIddictClientModels;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.UseCases;

internal sealed class LogInUseCase(
    OpenIddictClientService openIddictClientService,
    ILogger<LogInUseCase> logger) :
    ILogInUseCase
{
    public async Task LogIn(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        try
        {
            var request = new PasswordAuthenticationRequest
            {
                Username = email,
                Password = password
            };

            var result = await openIddictClientService.AuthenticateWithPasswordAsync(
                request);

            Console.Write(result.AccessToken);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                "An unexpected error occurred while logging in a user");

            throw;
        }
    }
}
