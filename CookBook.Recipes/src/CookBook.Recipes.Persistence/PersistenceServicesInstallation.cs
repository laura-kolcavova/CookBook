using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Persistence.Categories.Services;
using CookBook.Recipes.Persistence.Recipes.Services;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
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
            .AddScoped<ICategoryCommandService, CategoryCommandService>()
            .AddScoped<ICategoryQueryService, CategoryQueryService>();

        services
            .AddScoped<IRecipeCommandService, RecipeCommandService>()
            .AddScoped<IRecipeQueryService, RecipeQueryService>();

        return services;
    }
}
