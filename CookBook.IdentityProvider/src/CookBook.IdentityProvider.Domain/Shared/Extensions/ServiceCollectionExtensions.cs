using CookBook.IdentityProvider.Domain.Users.Services;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
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
        services
            .AddScoped<IUserManager, UserManager>();

        return services;
    }
}
