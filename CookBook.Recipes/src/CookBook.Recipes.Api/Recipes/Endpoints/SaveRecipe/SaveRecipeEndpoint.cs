using CookBook.Extensions.AspNetCore.Errors.Extensions;
using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;
using CookBook.Recipes.Api.Shared.Extensions;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Models;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Infrastructure.Shared.Configuration;
using FluentValidation;
using System.Security.Claims;
using RecipeErrors = CookBook.Recipes.Domain.Recipes.RecipeErrors;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe;

internal class SaveRecipeEndpoint
{
    public static void Configure(
        IEndpointRouteBuilder builder)
    {
        builder
            .MapPut("/save", HandleAsync)
            .WithName("SaveRecipe")
            .WithSummary("Updates a recipe or creates a new one if it does not exist")
            .WithDescription("This endpoint returns a DTO containing an id of created or edited recipe.")
            .Produces<SaveRecipeResponseDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.ReadWrite);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        SaveRecipeParams request,
        ClaimsPrincipal claimsPrincipal,
        IRecipeStore recipeStore,
        ILogger<SaveRecipeEndpoint> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {

        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = request.SaveRecipeRequest.RecipeId,
            ["Title"] = request.SaveRecipeRequest.Title,
        });

        try
        {
            var userName = claimsPrincipal.GetUserNameClaim().Value;

            if (request.SaveRecipeRequest.RecipeId is null ||
                request.SaveRecipeRequest.RecipeId <= 0)
            {
                var newRecipe = new RecipeAggregate(
                    request.SaveRecipeRequest.Title,
                    userName);

                SaveRecipeData(
                    newRecipe,
                    request.SaveRecipeRequest);

                await recipeStore.Create(
                    newRecipe,
                    cancellationToken);

                return TypedResults.Ok(
                    new SaveRecipeResponseDto
                    {
                        RecipeId = newRecipe.Id
                    });
            }

            var existingRecipe = await recipeStore.FindByRecipeId(
                    request.SaveRecipeRequest.RecipeId.Value,
                    cancellationToken);

            if (existingRecipe is null)
            {
                return TypedResults.Problem(
                    RecipeErrors
                        .Recipe
                        .NotFound(
                            request.SaveRecipeRequest.RecipeId.Value)
                        .AsProblemDetails(httpContext));
            }

            if (existingRecipe.UserName != userName)
            {
                return TypedResults.Problem(
                    RecipeErrors
                        .Recipe
                        .NotOwnedByUser(
                            existingRecipe.Id,
                            existingRecipe.UserName)
                        .AsProblemDetails(httpContext));
            }

            SaveRecipeData(
                existingRecipe,
                request.SaveRecipeRequest);

            await recipeStore.Update(
                existingRecipe,
                cancellationToken);

            return TypedResults.Ok(
                new SaveRecipeResponseDto
                {
                    RecipeId = existingRecipe.Id
                });
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while saving a recipe");

            throw;
        }
    }

    private static void SaveRecipeData(
        RecipeAggregate recipe,
        SaveRecipeRequestDto saveRecipeRequest)
    {
        var saveIngredientItems = saveRecipeRequest
            .Ingredients
            .Select(ingredient =>
                new SaveIngredientItemParams
                {
                    LocalId = ingredient.LocalId,
                    Note = ingredient.Note
                })
            .ToList();

        var saveInstructionItems = saveRecipeRequest
            .Instructions
            .Select(instruction =>
                new SaveInstructionItemParams
                {
                    LocalId = instruction.LocalId,
                    Note = instruction.Note
                })
            .ToList();

        recipe.SetTitle(saveRecipeRequest.Title);
        recipe.SetDescription(saveRecipeRequest.Description);
        recipe.SetServings(saveRecipeRequest.Servings);
        recipe.SetCookTime(saveRecipeRequest.CookTime);
        recipe.SetNotes(saveRecipeRequest.Notes);
        recipe.SaveIngredients(saveIngredientItems);
        recipe.SaveInstructions(saveInstructionItems);
        recipe.SaveTags(saveRecipeRequest.Tags);
    }
}
