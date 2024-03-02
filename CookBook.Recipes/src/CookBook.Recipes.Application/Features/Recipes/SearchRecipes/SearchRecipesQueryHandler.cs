using CookBook.Recipes.Domain.Recipes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Features.Recipes.SearchRecipes;

internal class SearchRecipesQueryHandler : IRequestHandler<
    SearchRecipesQuery,
    IReadOnlyCollection<RecipeListingItemReadModel>>
{
    private readonly IRecipeListingItemsRepository _recipeListingItemsRepository;
    private readonly ILogger<SearchRecipesQueryHandler> _logger;

    public SearchRecipesQueryHandler(
        IRecipeListingItemsRepository recipeListingItemsRepository,
        ILogger<SearchRecipesQueryHandler> logger
        )
    {
        _recipeListingItemsRepository = recipeListingItemsRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<RecipeListingItemReadModel>>
        Handle(SearchRecipesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var recipes = await _recipeListingItemsRepository.GetAllAsync(
                offsetFilter: request.OffsetFilter,
                cancellationToken: cancellationToken);

            return recipes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while searching recipes");
            throw;
        }
    }
}
