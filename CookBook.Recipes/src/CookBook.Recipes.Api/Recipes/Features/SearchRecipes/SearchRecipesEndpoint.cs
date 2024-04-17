﻿using CookBook.Recipes.Api.Shared.Dto;
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
            .MapGet("search", HandleAsync)
            .WithName("SearchRecipes")
            .WithSummary("Search for existing recipes")
            .WithDescription("Returns a DTO containing a collection of recipe listing item DTOs.")
            .Produces<SearchRecipesEndpointResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        OffsetFilterRequestDto offsetFilterRequest,
        IRecipeQueryService recipeQueryService,
        HttpContext httpContext,
        CancellationToken cancellationToken
        )
    {
        var offsetFilter =
            offsetFilterRequest.Limit is not null ||
            offsetFilterRequest.Offset is not null
            ? new OffsetFilter
            {
                Offset = offsetFilterRequest.Offset ?? 0,
                Limit = offsetFilterRequest.Limit ?? 100,
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

        var recipes = await recipeQueryService.SearchRecipesAsync(
            sorting,
            offsetFilter,
            cancellationToken);

        return Results.Ok(new SearchRecipesEndpointResponseDto
        {
            Recipes = recipes
        });
    }
}
