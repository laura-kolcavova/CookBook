using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Shared;

public abstract class ReadModel<TPrimaryKey> : Entity<TPrimaryKey>, IReadModel<TPrimaryKey>
{
}
