using CookBook.IdentityProvider.Domain.Users.ValueObjects;

namespace CookBook.IdentityProvider.Domain.Users;

public sealed class UserAggregate
{
    public Guid UserNumber { get; }

    public string DisplayName { get; }

    public LoginCredentialsValueObject LoginCredentials { get; private set; }

    public UserAggregate(
        Guid userNumber,
        string displayName)
    {
        UserNumber = userNumber;
        DisplayName = displayName;
        LoginCredentials = LoginCredentialsValueObject.Empty;
    }

    public void SetLoginCredentials(
        LoginCredentialsValueObject loginCredentials)
    {
        LoginCredentials = loginCredentials;
    }
}
