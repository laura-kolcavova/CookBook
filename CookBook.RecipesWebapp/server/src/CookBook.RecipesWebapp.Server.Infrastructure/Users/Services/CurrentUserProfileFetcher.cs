using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;
using Microsoft.Extensions.Logging;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.Services;

internal sealed class CurrentUserProfileFetcher(
    IIdentityProviderApiClient identityProviderApiClient,
    ILogger<CurrentUserProfileFetcher> logger) :
    ICurrentUserProfileFetcher
{
    public async Task<CurrentUserProfileModel> FetchCurrentUserProfile(
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await identityProviderApiClient.GetCurrentUserProfile(
                cancellationToken);

            if (!response.IsSuccessful)
            {
                throw response.Error!;
            }

            var currentUserProfile = response.Content!;

            return new CurrentUserProfileModel
            {
                UserName = currentUserProfile.UserName,
                DisplayName = currentUserProfile.DisplayName,
                Email = currentUserProfile.Email
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (ApiException ex)
        {
            logger.LogError(
                ex,
                "Getting current user profile from Identity Provider API failed with [{StatusCode}], [{Message}]",
                ex.StatusCode,
                ex.Message);

            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting current user profile from Identity Provider API");

            throw;
        }
    }
}
