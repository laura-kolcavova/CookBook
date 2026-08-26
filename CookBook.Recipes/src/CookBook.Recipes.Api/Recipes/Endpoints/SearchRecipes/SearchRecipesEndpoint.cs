using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;
using CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Mappers;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes;

internal class SearchRecipesEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("/search", HandleAsync)
            .WithName("SearchRecipes")
            .WithSummary("Search for existing recipes")
            .Produces<SearchRecipesResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        SearchRecipesParams request,
        ISearchRecipesQuery searchRecipesQuery,
        ILogger<SearchRecipesEndpoint> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["SearchTerm"] = request.SearchTerm
        });

        try
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
                new()
                {
                    PropertyName = nameof(RecipeSearchItemReadModel.CreatedAt),
                    Direction = SortingDirection.Descending
                }
            };

            var searchedRecipes = await searchRecipesQuery.Execute(
                request.SearchTerm,
                sorting,
                offsetFilter,
                cancellationToken);

            if (searchedRecipes.Count == 0)
            {
                return TypedResults.NoContent();
            }

            var responseDto = new SearchRecipesResponseDto
            {
                Recipes = searchedRecipes.ToDtoCollection()
            };

            return TypedResults.Ok(responseDto);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while searching for recipes");

            throw;
        }
    }
}
