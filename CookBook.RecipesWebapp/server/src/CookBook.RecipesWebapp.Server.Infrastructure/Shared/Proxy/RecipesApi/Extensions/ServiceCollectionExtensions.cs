using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipesApiProxy(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var recipesApiConfiguration = configuration
            .GetRequiredSection(nameof(RecipesApiConfiguration))
            .Get<RecipesApiConfiguration>()!;

        services
            .AddRefitClient<IRecipesClient>()
            .ConfigureHttpClient(configureClient =>
            {
                configureClient.BaseAddress = new Uri(
                    recipesApiConfiguration.BaseAddress);
            });

        return services;
    }
}
