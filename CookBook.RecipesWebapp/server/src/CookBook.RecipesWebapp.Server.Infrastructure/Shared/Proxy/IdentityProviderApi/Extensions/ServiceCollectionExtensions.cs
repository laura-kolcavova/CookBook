using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipesApiProxy(
       this IServiceCollection services,
       IConfigurationManager configuration)
    {
        var identityProviderApiConfiguration = configuration
            .GetRequiredSection(nameof(IdentityProviderApiConfiguration))
            .Get<IdentityProviderApiConfiguration>()!;

        services
            .AddRefitClient<IIdentityProviderClient>()
            .ConfigureHttpClient(configureClient =>
            {
                configureClient.BaseAddress = new Uri(
                    identityProviderApiConfiguration.BaseAddress);
            });

        return services;
    }
}
