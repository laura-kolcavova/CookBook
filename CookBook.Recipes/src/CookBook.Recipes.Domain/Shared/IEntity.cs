namespace CookBook.Recipes.Domain.Shared;

public interface IEntity<out TPrimaryKey>
     where TPrimaryKey : notnull
{
    public abstract TPrimaryKey GetPrimaryKey();
}
