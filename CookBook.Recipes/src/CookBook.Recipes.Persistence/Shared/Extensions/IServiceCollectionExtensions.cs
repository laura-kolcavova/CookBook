using CookBook.Recipes.Domain.Recipes.Services;
using CookBook.Recipes.Persistence.Recipes;
using CookBook.Recipes.Persistence.Recipes.Services;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Persistence.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string cookBookRecipesConnectionString,
        bool isDevelopment)
    {
        services
           .AddHealthChecks()
           .AddSqlServer(
               cookBookRecipesConnectionString,
               name: "CookBookRecipes_DB",
               tags: new[]
               {
                    "readiness"
               });

        services
            .AddRecipes(
                cookBookRecipesConnectionString,
                isDevelopment);

        services
            .AddSingleton<UpdateTrackingFieldsInterceptor>();

        return services;
    }

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
            .AddScoped<ISaveRecipeService, SaveRecipeService>()
            .AddScoped<IRemoveRecipeService, RemoveRecipeService>()
            .AddScoped<ISearchRecipesService, SearchRecipesService>()
            .AddScoped<IGetRecipeDetailService, GetRecipesDetailService>();

        return services;
    }
}
