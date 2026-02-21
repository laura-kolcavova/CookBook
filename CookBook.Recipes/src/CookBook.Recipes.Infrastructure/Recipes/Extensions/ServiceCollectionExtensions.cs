using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Infrastructure.Recipes.Services;
using CookBook.Recipes.Infrastructure.Shared.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Infrastructure.Recipes.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipes(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
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
            .AddScoped<IRecipeStore, RecipeStore>();

        services
            .AddScoped<IGetRecipeDetailQuery, GetRecipeDetailQuery>()
            .AddScoped<IGetLatestRecipesQuery, GetLatestRecipesQuery>()
            .AddScoped<ISearchRecipesQuery, SearchRecipesQuery>();

        return services;
    }
}
