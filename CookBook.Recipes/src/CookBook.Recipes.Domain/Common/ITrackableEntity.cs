namespace CookBook.Recipes.Domain.Common;

public interface ITrackableEntity
{
    DateTimeOffset? CreatedAt { get; }

    DateTimeOffset? UpdatedAt { get; }
}
