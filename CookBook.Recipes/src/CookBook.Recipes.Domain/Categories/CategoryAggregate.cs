using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Categories;

public sealed class CategoryAggregate : AggregateRoot<int>, ITrackableEntity
{
    public const int RootCategoryId = 0;

    public int Id { get; }

    public string Name { get; private set; }

    public int ParentCategoryId { get; private set; }

    public DateTimeOffset? CreatedAt { get; }

    public DateTimeOffset? UpdatedAt { get; }

    public CategoryAggregate(string name)
    {
        Name = name;
        ParentCategoryId = RootCategoryId;
    }

    public CategoryAggregate(string name, CategoryAggregate parentCategory)
    {
        Name = name;
        ParentCategoryId = parentCategory.Id;
    }

    public override int GetPrimaryKey() => Id;
}
