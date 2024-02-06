using CookBook.Recipes.Application.Common;
using CookBook.Recipes.Domain.Entities.Recipes;

namespace CookBook.Recipes.Application.Services;
public interface IRecipeService : IAggregateRootRepository<RecipeAggregate, long>
{
}
