using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services
            .AddRecipesApiProxy(configuration);

        return services;
    }
}
