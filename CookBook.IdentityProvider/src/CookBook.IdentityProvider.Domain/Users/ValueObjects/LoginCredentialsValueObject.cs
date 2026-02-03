using CSharpFunctionalExtensions;

namespace CookBook.IdentityProvider.Domain.Users.ValueObjects;

public sealed class LoginCredentialsValueObject : ValueObject
{
    public string Email { get; }

    public string PasswordHash { get; }

    private LoginCredentialsValueObject(
        string email,
        string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return PasswordHash;
    }

    public static LoginCredentialsValueObject Create(
        string email,
        string passwordHash)
    {
        return new LoginCredentialsValueObject(
            email,
            passwordHash);
    }
    public static LoginCredentialsValueObject Empty => new(
        string.Empty,
        string.Empty);
}
