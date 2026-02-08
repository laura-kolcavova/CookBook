namespace CookBook.Recipes.Domain.Shared.Entities;

public interface ITrackableEntity
{
    DateTimeOffset? CreatedAt { get; }

    DateTimeOffset? UpdatedAt { get; }
}
