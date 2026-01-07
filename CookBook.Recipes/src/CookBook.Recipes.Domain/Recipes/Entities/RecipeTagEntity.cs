using CookBook.Recipes.Domain.Recipes.Models;
using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes.Entities;

public sealed class RecipeTagEntity :
    Entity
{
    public long RecipeId { get; }

    public string Name { get; }

    public RecipeTagEntity(
        string name)
    {
        Name = name;
    }

    public override object GetPrimaryKey() => new RecipeTagPrimaryKey
    {
        Name = Name,
        RecipeId = RecipeId
    };
}
