namespace CookBook.Recipes.Domain.Common;

public interface IAggregateRoot : IEntity
{
}

public interface IAggregateRoot<out TPrimaryKey> : IAggregateRoot, IEntity<TPrimaryKey>
{
}
