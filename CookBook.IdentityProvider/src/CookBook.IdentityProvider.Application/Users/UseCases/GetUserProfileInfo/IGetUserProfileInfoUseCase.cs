using CookBook.IdentityProvider.Domain.Users.ReadModels;
using CSharpFunctionalExtensions;

namespace CookBook.IdentityProvider.Application.Users.UseCases.GetUserProfileInfo;

public interface IGetUserProfileInfoUseCase
{
    public Task<Maybe<UserProfileInfoReadModel>> GetUserProfileInfo(
        string userName,
        CancellationToken cancellationToken);
}
