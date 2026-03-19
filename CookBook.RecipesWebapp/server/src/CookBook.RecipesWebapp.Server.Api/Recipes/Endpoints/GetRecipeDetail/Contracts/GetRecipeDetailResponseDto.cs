namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;

internal sealed record GetRecipeDetailResponseDto
{
    public required RecipeDetailDto RecipeDetail { get; init; }
}
