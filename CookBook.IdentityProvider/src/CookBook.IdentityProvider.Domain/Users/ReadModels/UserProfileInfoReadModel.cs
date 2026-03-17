namespace CookBook.IdentityProvider.Domain.Users.ReadModels;

public sealed record UserProfileInfoReadModel
{
    public required string UserName { get; init; }

    public required string DisplayName { get; init; }

}
