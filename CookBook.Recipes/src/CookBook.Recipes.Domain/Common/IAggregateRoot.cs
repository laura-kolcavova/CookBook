namespace CookBook.Recipes.Domain.Common;

public interface IAggregateRoot<out TPrimaryKey> : IEntity<TPrimaryKey>
{
}
