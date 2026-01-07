using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

internal static class SearchRecipesEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapGet("/Search", HandleAsync)
            .WithName("SearchRecipes")
            .WithSummary("Search for existing recipes")
            .WithDescription("Returns a DTO containing a collection of recipe listing item DTOs.")
            .Produces<SearchRecipesResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        SearchRecipesEndpointParams request,
        ISearchRecipesService searchRecipesService,
        HttpContext httpContext,
        CancellationToken cancellationToken
        )
    {
        var offsetFilter =
            request.Offset is not null ||
            request.Limit is not null
            ? new OffsetFilter
            {
                Offset = request.Offset ?? 0,
                Limit = request.Limit ?? 100,
            }
            : null;

        var sorting = new List<SortBy>()
        {
            new SortBy()
            {
                PropertyName = nameof(RecipeListingItemReadModel.Title),
                Direction = SortingDirection.Ascending
            }
        };

        var recipes = await searchRecipesService.SearchRecipes(
            sorting,
            offsetFilter,
            cancellationToken);

        if (recipes.Count == 0)
        {
            return TypedResults.NoContent();
        }

        var responseDto = new SearchRecipesResponseDto
        {
            Recipes = recipes
        };

        return TypedResults.Ok(responseDto);
    }
}
