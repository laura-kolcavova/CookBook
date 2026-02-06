using CookBook.IdentityProvider.Domain.Shared.Entities;

namespace CookBook.IdentityProvider.Domain.Users;

public sealed class UserAggregate :
    AggregateRoot,
    ITrackableEntity
{
    public int Id { get; }

    public Guid UserNumber { get; }

    public string DisplayName { get; }

    public int IdentityUserId { get; }

    public DateTimeOffset? CreatedAt { get; }

    public DateTimeOffset? UpdatedAt { get; }

    public UserAggregate(
        Guid userNumber,
        string displayName,
        int identityUserId)
    {
        UserNumber = userNumber;
        DisplayName = displayName;
        IdentityUserId = identityUserId;
    }

    public override object GetPrimaryKey()
    {
        return Id;
    }
}
