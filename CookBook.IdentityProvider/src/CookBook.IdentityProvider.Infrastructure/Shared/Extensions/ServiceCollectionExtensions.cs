using CookBook.IdentityProvider.Infrastructure.Shared.Interceptors;
using CookBook.IdentityProvider.Infrastructure.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Infrastructure.Shared.Extensions;

public static class ServiceCollectionExtensions
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
            .AddIdentityUsers(
                connectionString,
                isDevelopment);

        services
            .AddUsers(
                connectionString,
                isDevelopment);

        return services;
    }
}
