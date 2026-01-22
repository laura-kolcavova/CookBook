using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;
using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Mappers;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

internal static class SearchRecipesEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapGet("/search", HandleAsync)
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
        ISearchRecipesUseCase searchRecipesUseCase,
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
                PropertyName = nameof(RecipeSearchItemReadModel.CreatedAt),
                Direction = SortingDirection.Descending
            }
        };

        var recipes = await searchRecipesUseCase.SearchRecipes(
            request.SearchTerm,
            sorting,
            offsetFilter,
            cancellationToken);

        if (recipes.Count == 0)
        {
            return TypedResults.NoContent();
        }

        var responseDto = new SearchRecipesResponseDto
        {
            Recipes = recipes.ToDtoCollection()
        };

        return TypedResults.Ok(responseDto);
    }
}
