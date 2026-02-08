namespace CookBook.IdentityProvider.Domain.Shared.Entities;

public interface IEntity
{
    public abstract object GetPrimaryKey();
}
