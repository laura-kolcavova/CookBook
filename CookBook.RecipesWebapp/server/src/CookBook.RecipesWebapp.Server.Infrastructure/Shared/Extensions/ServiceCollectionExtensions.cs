using CookBook.RecipesWebapp.Server.Infrastructure.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddUsers();

        return services;
    }
}
