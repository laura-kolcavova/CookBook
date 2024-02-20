using CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

namespace CookBook.Recipes.Api.Features.Recipes;

internal static class RecipesEndpoints
{
    public static RouteGroupBuilder AddRecipesEndpoints(this RouteGroupBuilder group)
    {
        var recipesGroup = group
            .MapGroup("/recipes")
            .WithTags("Recipes");

        recipesGroup
            .MapPost("save-recipe", SaveRecipeEndpoint.HandleAsync)
            .WithName("SaveRecipe")
            .WithSummary("Saves a newly created recipe or saves changes of already existing recipe")
            .WithDescription("This endpoint returns a DTO containing an id of created or edited recipe.")
            .Produces<SaveRecipeResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesValidationProblem();

        return group;
    }
}
