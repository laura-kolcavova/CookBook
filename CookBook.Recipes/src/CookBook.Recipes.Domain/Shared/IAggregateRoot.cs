namespace CookBook.Recipes.Domain.Shared;

public interface IAggregateRoot<out TPrimaryKey> : IEntity<TPrimaryKey>
{
}
