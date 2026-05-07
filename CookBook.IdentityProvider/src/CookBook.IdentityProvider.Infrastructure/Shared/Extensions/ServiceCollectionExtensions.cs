using CookBook.Extensions.AspNetCore.SqlServer.Extensions;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using CookBook.IdentityProvider.Infrastructure.Shared.Interceptors;
using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Extensions;
using CookBook.IdentityProvider.Infrastructure.Users.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Infrastructure.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationManager configuration,
        bool isDevelopment)
    {
        var connectionString = configuration.GetSqlConnectionString(
            ConfigurationConstants.SqlConnectionStrings.CookBookIdentityProviderSectionName);

        services
           .AddHealthChecks()
           .AddSqlServer(
                connectionString,
                name: "CookBookIdentityProvider_DB",
                tags: [
                    "readiness"
                ]);

        services
            .AddSingleton<UpdateTrackingFieldsInterceptor>();

        services
            .AddUsers(
                connectionString,
                isDevelopment);

        services
            .AddOpenIddictServer(
                configuration,
                isDevelopment);

        return services;
    }
}
