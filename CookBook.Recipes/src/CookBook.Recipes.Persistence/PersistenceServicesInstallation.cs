using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Recipes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Persistence;

public static class PersistenceServicesInstallation
{
    public static IServiceCollection InstallPersistenceServices(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services.AddScoped(_ => new RecipesContext(
            connectionString: connectionString,
            useDevelopmentLogging: isDevelopment));

        services
            .AddScoped<IRecipeService, RecipeService>()
            .AddScoped<IRecipeListingItemReadModelService, RecipeListingItemReadModelService>()
            .AddScoped<IRecipeDetailReadModelService, RecipeDetailReadModelService>();

        return services;
    }
}
