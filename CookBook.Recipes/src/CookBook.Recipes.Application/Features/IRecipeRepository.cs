using CookBook.Recipes.Application.Common;
using CookBook.Recipes.Domain.Recipes;

namespace CookBook.Recipes.Application.Features;

public interface IRecipeRepository : IAggregateRootRepository<RecipeAggregate, long>
{
}
