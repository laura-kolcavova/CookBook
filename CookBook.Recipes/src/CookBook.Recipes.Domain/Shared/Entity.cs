namespace CookBook.Recipes.Domain.Shared;

public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>, IEquatable<Entity<TPrimaryKey>>
    where TPrimaryKey : notnull
{
    public static bool operator ==(Entity<TPrimaryKey>? first, Entity<TPrimaryKey>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity<TPrimaryKey>? first, Entity<TPrimaryKey>? second)
    {
        return !(first == second);
    }

    public bool Equals(Entity<TPrimaryKey>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != this.GetType())
        {
            return false;
        }

        return EqualityComparer<TPrimaryKey>.Default.Equals(other.GetPrimaryKey(), this.GetPrimaryKey());
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return obj is Entity<TPrimaryKey> entity &&
               EqualityComparer<TPrimaryKey>.Default.Equals(entity.GetPrimaryKey(), this.GetPrimaryKey());
    }

    public override int GetHashCode()
    {
        return this.GetPrimaryKey().GetHashCode() * 41;
    }

    public abstract TPrimaryKey GetPrimaryKey();
}
