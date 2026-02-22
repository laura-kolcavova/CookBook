using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Users.UseCases.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Application.Users.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUser(
        this IServiceCollection services)
    {
        services
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        return services;
    }
}
