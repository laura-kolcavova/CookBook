using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityProviderApiProxy(
       this IServiceCollection services,
       IConfigurationManager configuration)
    {
        var identityProviderApiConfiguration = configuration
            .GetRequiredSection(nameof(IdentityProviderApiConfiguration))
            .Get<IdentityProviderApiConfiguration>()!;

        services.AddHttpContextAccessor();

        services.AddTransient<AccessTokenDelegatingHandler>();

        services
            .AddRefitClient<IIdentityProviderApiClient>()
            .ConfigureHttpClient(configureClient =>
            {
                configureClient.BaseAddress = new Uri(
                    identityProviderApiConfiguration.BaseAddress);
            })
            .AddHttpMessageHandler<AccessTokenDelegatingHandler>();

        return services;
    }
}
