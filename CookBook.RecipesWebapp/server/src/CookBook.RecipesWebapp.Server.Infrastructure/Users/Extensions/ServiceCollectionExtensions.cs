using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using CookBook.RecipesWebapp.Server.Infrastructure.Users.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
        this IServiceCollection services)
    {
        services
            .AddScoped<ILogInUseCase, LogInUseCase>();

        return services;
    }
}
