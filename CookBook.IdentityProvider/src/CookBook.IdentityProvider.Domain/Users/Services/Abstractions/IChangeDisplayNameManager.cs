namespace CookBook.IdentityProvider.Domain.Users.Services.Abstractions;

public interface IChangeDisplayNameManager
{
    public Task ChangeDisplayName(
        int identityUserId,
        string displayName,
        CancellationToken cancellationToken);
}
