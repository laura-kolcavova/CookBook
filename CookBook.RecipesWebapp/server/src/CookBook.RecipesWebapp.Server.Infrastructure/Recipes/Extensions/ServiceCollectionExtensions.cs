using CookBook.RecipesWebapp.Server.Domain.Recipes.Services.Abastractions;
using CookBook.RecipesWebapp.Server.Infrastructure.Recipes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Recipes.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipes(
        this IServiceCollection services)
    {
        services
            .AddScoped<IRecipeDetailFetcher, RecipeDetailFetcher>();

        return services;
    }
}
