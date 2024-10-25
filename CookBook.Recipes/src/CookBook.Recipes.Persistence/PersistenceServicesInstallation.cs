using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Persistence.Categories.Services;
using CookBook.Recipes.Persistence.Recipes.Services;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Persistence;

public static class PersistenceServicesInstallation
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services
            .AddSingleton<UpdateTrackingFieldsInterceptor>();

        services
            .AddScoped(serviceProvider =>
            {
                var updateTrackingFieldsInterceptor = serviceProvider
                    .GetRequiredService<UpdateTrackingFieldsInterceptor>();

                return new RecipesContext(
                    connectionString: connectionString,
                    useDevelopmentLogging: isDevelopment,
                    updateTrackingFieldsInterceptor: updateTrackingFieldsInterceptor);
            });

        services
            .AddScoped<ICategoryCommandService, CategoryCommandService>()
            .AddScoped<ICategoryQueryService, CategoryQueryService>();

        services
            .AddScoped<IRecipeCommandService, RecipeCommandService>()
            .AddScoped<IRecipeQueryService, RecipeQueryService>();

        return services;
    }
}
