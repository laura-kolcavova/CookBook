using CookBook.Recipes.Api.Recipes.Features.SearchRecipes.Contracts;
using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Api.Recipes.Features.SearchRecipes;

internal static class SearchRecipesEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapGet("/Search", HandleAsync)
            .WithName("SearchRecipes")
            .WithSummary("Search for existing recipes")
            .WithDescription("Returns a DTO containing a collection of recipe listing item DTOs.")
            .Produces<SearchRecipesEndpointResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        SearchRecipesParams request,
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

        var responseDto = new SearchRecipesEndpointResponseDto
        {
            Recipes = recipes
        };

        return TypedResults.Ok(responseDto);
    }
}
