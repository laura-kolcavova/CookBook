using CookBook.Recipes.Application.Recipes.UseCases.GetLatestRecipes;
using CookBook.Recipes.Application.Recipes.UseCases.GetRecipeDetail;
using CookBook.Recipes.Application.Recipes.UseCases.RemoveRecipe;
using CookBook.Recipes.Application.Recipes.UseCases.SaveRecipe;
using CookBook.Recipes.Application.Recipes.UseCases.SearchRecipes;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Application.Recipes.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipes(
        this IServiceCollection services)
    {
        services
            .AddScoped<ISaveRecipeUseCase, SaveRecipeUseCase>()
            .AddScoped<IRemoveRecipeUseCase, RemoveRecipeUseCase>()
            .AddScoped<IGetRecipeDetailUseCase, GetRecipeDetailUseCase>()
            .AddScoped<IGetLatestRecipesUseCase, GetLatestRecipesUseCase>()
            .AddScoped<ISearchRecipesUseCase, SearchRecipesUseCase>();

        return services;
    }
}
