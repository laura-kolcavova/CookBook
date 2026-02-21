using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using CookBook.RecipesWebapp.Server.Infrastructure.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
        this IServiceCollection services)
    {
        services
            .AddScoped<IAuthenticationManager, AuthenticationManager>();

        return services;
    }
}
