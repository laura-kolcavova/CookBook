namespace CookBook.Recipes.Domain.Common;

public interface IEntity<out TPrimaryKey>
{
    TPrimaryKey Id { get; }
}
