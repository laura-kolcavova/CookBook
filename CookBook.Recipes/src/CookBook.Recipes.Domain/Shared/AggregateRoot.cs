namespace CookBook.Recipes.Domain.Common;

public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
{
}
