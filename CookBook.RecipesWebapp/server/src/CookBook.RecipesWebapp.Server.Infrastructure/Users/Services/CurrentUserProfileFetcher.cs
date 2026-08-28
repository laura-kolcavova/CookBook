using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.Services;

internal sealed class CurrentUserProfileFetcher(
    IIdentityProviderApiClient identityProviderApiClient,
    IHttpContextAccessor httpContextAccessor,
    ILogger<CurrentUserProfileFetcher> logger) :
    ICurrentUserProfileFetcher
{
    public async Task<CurrentUserProfileModel> FetchCurrentUserProfile(
        CancellationToken cancellationToken)
    {
        if (httpContextAccessor.HttpContext is null)
        {
            throw new InvalidOperationException(
                "FetchCurrentUserProfile represents the current user of an HTTP request and cannot be called outside of one, e.g. from a background job.");
        }

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
