namespace CookBook.IdentityProvider.Domain.Users.Services.Abstractions;

public interface IChangeDisplayNameManager
{
    public Task ChangeDisplayName(
        CustomIdentityUser identityUser,
        string displayName,
        CancellationToken cancellationToken);
}
