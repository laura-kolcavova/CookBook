using CookBook.Recipes.Persistence.Recipes.Extensions;
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
          .AddSingleton<UpdateTrackingFieldsInterceptor>();

        services
            .AddRecipes(
                connectionString,
                isDevelopment);

        return services;
    }
}
