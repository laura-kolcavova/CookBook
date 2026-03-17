using CookBook.IdentityProvider.Domain.Users.ReadModels;

namespace CookBook.IdentityProvider.Domain.Users.Services.Abstractions;

public interface IGetUserProfileInfoQuery
{
    public Task<UserProfileInfoReadModel?> Execute(
        string userName,
        CancellationToken cancellationToken);
}
