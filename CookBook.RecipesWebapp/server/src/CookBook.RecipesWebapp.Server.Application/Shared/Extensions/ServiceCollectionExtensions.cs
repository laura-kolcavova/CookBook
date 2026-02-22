using CookBook.RecipesWebapp.Server.Application.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Application.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddUsers();

        return services;
    }
}
