using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Domain.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddUsers();

        return services;
    }

    internal static IServiceCollection AddUsers(this IServiceCollection services)
    {
        return services;
    }
}
