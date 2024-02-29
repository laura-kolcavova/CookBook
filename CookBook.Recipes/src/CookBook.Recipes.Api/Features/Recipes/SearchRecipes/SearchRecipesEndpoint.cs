using CookBook.Recipes.Api.Features.Recipes.Shared.Dto;
using CookBook.Recipes.Api.Features.Shared.Dto;
using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Features.Recipes.SearchRecipes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Features.Recipes.SearchRecipes;

internal static class SearchRecipesEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapPost(string.Empty, HandleAsync)
            .WithName("SearchRecipe")
            .WithSummary("Search for existing recipes")
            .WithDescription("This endpoint returns a DTO containing a collection of recipe listing item DTOs.")
            .Produces<SearchRecipesEndpointResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [FromQuery] OffsetFilterRequestDto? offsetFilterRequest,
        IMediator mediator,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var offsetFilter = offsetFilterRequest != null
            ? new OffsetFilter
            {
                Offset = offsetFilterRequest.Offset ?? 0,
                Limit = offsetFilterRequest.Limit ?? int.MaxValue,
            }
            : null;

        var recipes = await mediator
            .Send(new SearchRecipesQuery
            {
                OffsetFilter = offsetFilter
            }, cancellationToken);

        return Results.Ok(new SearchRecipesEndpointResponse
        {
            Recipes = recipes.Select(recipe => new RecipeListingItemDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
            })
            .ToList()
        });
    }
}
