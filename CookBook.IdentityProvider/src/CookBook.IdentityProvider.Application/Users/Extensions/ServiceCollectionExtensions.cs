using CookBook.IdentityProvider.Application.Users.UseCases.GetUserProfileInfo;
using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Application.Users.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
        this IServiceCollection services)
    {
        services
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
            .AddScoped<IGetUserProfileInfoUseCase, GetUserProfileInfoUseCase>();

        return services;
    }
}
