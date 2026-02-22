using CookBook.IdentityProvider.Domain.Shared.Entities;

namespace CookBook.IdentityProvider.Domain.Users;

public sealed class UserAggregate :
    AggregateRoot,
    ITrackableEntity
{
    public int Id { get; }

    public int IdentityUserId { get; }

    public string UserName { get; }

    public string DisplayName { get; }

    public DateTimeOffset? CreatedAt { get; }

    public DateTimeOffset? UpdatedAt { get; }

    public UserAggregate(
        int identityUserId,
        string userName,
        string displayName)
    {
        IdentityUserId = identityUserId;
        UserName = userName;
        DisplayName = displayName;
    }

    public override object GetPrimaryKey()
    {
        return Id;
    }
}
