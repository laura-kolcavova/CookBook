namespace CookBook.Recipes.Domain.Shared;

public abstract class Entity :
    IEntity,
    IEquatable<Entity>
{
    public abstract object GetPrimaryKey();

    public static bool operator ==(Entity? first, Entity? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != this.GetType())
        {
            return false;
        }

        return other.GetPrimaryKey().Equals(this.GetPrimaryKey());
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

        return obj is Entity entity &&
               entity.GetPrimaryKey().Equals(this.GetPrimaryKey());
    }

    public override int GetHashCode()
    {
        return this.GetPrimaryKey().GetHashCode() * 41;
    }
}
