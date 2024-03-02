using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Features.Recipes.RemoveRecipe;

internal class RemoveRecipeCommandHandler : IRequestHandler<RemoveRecipeCommand, UnitResult<ExpectedError>>
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILogger<RemoveRecipeCommandHandler> _logger;

    public RemoveRecipeCommandHandler(
        IRecipeRepository recipeRepository,
        ILogger<RemoveRecipeCommandHandler> logger)
    {
        _recipeRepository = recipeRepository;
        _logger = logger;
    }

    public async Task<UnitResult<ExpectedError>> Handle(RemoveRecipeCommand request, CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["RecipeId"] = request.RecipeId,
        });

        try
        {
            var recipeExists = await _recipeRepository
                .ExistsAsync(request.RecipeId, cancellationToken);

            if (!recipeExists)
            {
                return RecipeErrors.NotFound(request.RecipeId);
            }

            await _recipeRepository.ExecuteRemoveAsync(request.RecipeId, cancellationToken);

            return UnitResult.Success<ExpectedError>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while removing a recipe");
            throw;
        }
    }
}
