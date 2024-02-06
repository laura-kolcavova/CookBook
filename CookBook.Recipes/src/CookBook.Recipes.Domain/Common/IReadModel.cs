namespace CookBook.Recipes.Domain.Common;

public interface IReadModel : IEntity
{
}

public interface IReadModel<out TPrimaryKey> : IReadModel, IEntity<TPrimaryKey>
{
}

