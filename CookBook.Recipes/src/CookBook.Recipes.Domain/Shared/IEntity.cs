namespace CookBook.Recipes.Domain.Shared;

public interface IEntity<out TPrimaryKey>
{
    TPrimaryKey Id { get; }
}
