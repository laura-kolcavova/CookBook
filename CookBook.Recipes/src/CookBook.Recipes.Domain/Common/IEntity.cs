namespace CookBook.Recipes.Domain.Common;

public interface IEntity
{
}

public interface IEntity<out TPrimaryKey> : IEntity
{
    TPrimaryKey Id { get; }
}
