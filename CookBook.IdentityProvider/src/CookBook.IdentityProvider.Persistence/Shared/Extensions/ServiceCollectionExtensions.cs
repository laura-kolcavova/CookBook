using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using CookBook.IdentityProvider.Persistence.Shared.Interceptors;
using CookBook.IdentityProvider.Persistence.Users;
using CookBook.IdentityProvider.Persistence.Users.UseCases.RegisterUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.IdentityProvider.Persistence.Shared.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services
           .AddHealthChecks()
           .AddSqlServer(
               connectionString,
               name: "CookBookRecipes_DB",
               tags: new[]
               {
                    "readiness"
               });

        services
            .AddIdentityUsers(
                connectionString,
                isDevelopment)
            .AddUsers(
                connectionString,
                isDevelopment);

        services
            .AddSingleton<UpdateTrackingFieldsInterceptor>();

        return services;
    }

    public static IServiceCollection AddIdentityUsers(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services
            .AddScoped(serviceProvider =>
            {
                return new IdentityUsersContext(
                    connectionString: connectionString,
                    useDevelopmentLogging: isDevelopment);
            });

        services
            .AddIdentityCore<IdentityUser<int>>()
            .AddEntityFrameworkStores<IdentityUsersContext>();

        return services;
    }

    public static IServiceCollection AddUsers(
       this IServiceCollection services,
       string connectionString,
       bool isDevelopment)
    {
        services
           .AddScoped(serviceProvider =>
           {
               var updateTrackingFieldsInterceptor = serviceProvider
                   .GetRequiredService<UpdateTrackingFieldsInterceptor>();

               return new UsersContext(
                   connectionString: connectionString,
                   useDevelopmentLogging: isDevelopment,
                   updateTrackingFieldsInterceptor: updateTrackingFieldsInterceptor);
           });

        services
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        return services;
    }
}
