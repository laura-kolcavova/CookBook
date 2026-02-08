using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Persistence.Recipes;
using CookBook.Recipes.Persistence.Recipes.UseCases;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Persistence.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services
           .AddHealthChecks()
           .AddSqlServer(
               connectionString,
               name: "CookBookRecipes_DB",
               tags: new[]
               {
                    "readiness"
               });

        services
            .AddRecipes(
                connectionString,
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
            .AddScoped<ISaveRecipeUseCase, SaveRecipeUseCase>()
            .AddScoped<IRemoveRecipeUseCase, RemoveRecipeUseCase>()
            .AddScoped<ISearchRecipesUseCase, SearchRecipesUseCase>()
            .AddScoped<IGetLatestRecipesUseCase, GetLatestRecipesUseCase>()
            .AddScoped<IGetRecipeDetailUseCase, GetRecipesDetailUseCase>();

        return services;
    }
}
