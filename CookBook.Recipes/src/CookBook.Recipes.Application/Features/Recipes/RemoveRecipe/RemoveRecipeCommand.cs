using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;
using MediatR;

namespace CookBook.Recipes.Application.Features.Recipes.RemoveRecipe;

public record RemoveRecipeCommand : IRequest<UnitResult<ExpectedError>>
{
    public required long RecipeId { get; init; }
}
