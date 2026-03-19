using CookBook.IdentityProvider.Domain.Users.ReadModels;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.IdentityProvider.Application.Users.UseCases.GetUserProfileInfo;

internal sealed class GetUserProfileInfoUseCase(
    IGetUserProfileInfoQuery getUserProfileInfoQuery,
    ILogger<GetUserProfileInfoUseCase> logger) :
    IGetUserProfileInfoUseCase
{
    public async Task<Maybe<UserProfileInfoReadModel>> GetUserProfileInfo(
        string userName,
        CancellationToken cancellationToken)
    {
        try
        {
            var userProfileInfo = await getUserProfileInfoQuery.Execute(
                userName,
                cancellationToken);

            if (userProfileInfo is null)
            {
                return Maybe.None;
            }

            return userProfileInfo;
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting user profile info");

            throw;
        }
    }
}
