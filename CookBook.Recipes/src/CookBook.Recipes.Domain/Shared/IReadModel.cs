namespace CookBook.Recipes.Domain.Common;

public interface IReadModel<out TPrimaryKey> : IEntity<TPrimaryKey>
{
}

