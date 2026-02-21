using CookBook.Recipes.Infrastructure.Recipes.Extensions;
using CookBook.Recipes.Infrastructure.Shared.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Infrastructure.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
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
