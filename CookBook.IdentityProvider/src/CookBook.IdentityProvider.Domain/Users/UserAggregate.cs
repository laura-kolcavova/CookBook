using CookBook.IdentityProvider.Domain.Shared.Entities;

namespace CookBook.IdentityProvider.Domain.Users;

public sealed class UserAggregate :
    AggregateRoot,
    ITrackableEntity
{
    public int Id { get; }

    public int IdentityUserId { get; }

    public Guid UserNumber { get; }

    public string DisplayName { get; }

    public DateTimeOffset? CreatedAt { get; }

    public DateTimeOffset? UpdatedAt { get; }

    public UserAggregate(
        int identityUserId,
        Guid userNumber,
        string displayName)
    {
        IdentityUserId = identityUserId;
        UserNumber = userNumber;
        DisplayName = displayName;
    }

    public override object GetPrimaryKey()
    {
        return Id;
    }
}
