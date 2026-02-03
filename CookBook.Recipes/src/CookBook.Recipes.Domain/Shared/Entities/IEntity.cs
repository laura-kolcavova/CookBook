namespace CookBook.Recipes.Domain.Shared.Entities;

public interface IEntity
{
    public abstract object GetPrimaryKey();
}
