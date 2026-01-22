using CookBook.Extensions.AspNetCore.Errors;
using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;
using CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Mappers;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using FluentValidation;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe;

internal static class SaveRecipeEndpoint
{
    public static void Configure(RouteGroupBuilder recipesGroup)
    {
        recipesGroup
            .MapPut("/save", HandleAsync)
            .WithName("SaveRecipe")
            .WithSummary("Updates a recipe or creates a new one if it does not exist")
            .WithDescription("This endpoint returns a DTO containing an id of created or edited recipe.")
            .Produces<SaveRecipeResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        SaveRecipeEndpointParams request,
        ISaveRecipeUseCase saveRecipeUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var saveRecipeParams = request
            .SaveRecipeRequest
            .ToSaveRecipeParams();

        var saveRecipeResult = await saveRecipeUseCase.SaveRecipe(
            saveRecipeParams,
            cancellationToken);

        if (saveRecipeResult.IsFailure)
        {
            return TypedResults.Problem(
                saveRecipeResult.Error.AsProblemDetails(httpContext));
        }

        var responseDto = saveRecipeResult.Value.ToDto();

        return TypedResults.Ok(responseDto);
    }
}
