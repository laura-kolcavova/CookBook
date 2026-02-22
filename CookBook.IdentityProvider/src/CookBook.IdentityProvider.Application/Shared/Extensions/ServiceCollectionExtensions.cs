using CookBook.IdentityProvider.Application.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Application.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddUser();

        return services;
    }
}
