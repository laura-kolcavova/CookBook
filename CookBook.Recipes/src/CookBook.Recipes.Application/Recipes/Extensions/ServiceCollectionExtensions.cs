using CookBook.Recipes.Application.Recipes.UseCases;
using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
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
