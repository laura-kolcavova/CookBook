using CookBook.Recipes.Application.Common;
using CookBook.Recipes.Domain.Entities.Recipes;

namespace CookBook.Recipes.Application.Repositories;

public interface IRecipeRepository : IAggregateRootRepository<RecipeAggregate, long>
{
}
