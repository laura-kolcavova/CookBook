namespace CookBook.Recipes.Domain.Shared;

public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    where TPrimaryKey : notnull
{
}
