using CookBook.RecipesWebapp.Server.Application.Users.UseCases;
using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Application.Users.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
       this IServiceCollection services)
    {
        services
            .AddScoped<IAuthenticateUserUseCase, AuthenticateUserUseCase>();

        return services;
    }
}
