using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Shared.Interceptors;
using CookBook.IdentityProvider.Infrastructure.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Infrastructure.Users.Extensions;

internal static class ServiceCollectionExtensions
{
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
            .AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                    options.ClaimsIdentity.RoleClaimType = Claims.Role;
                    options.ClaimsIdentity.EmailClaimType = Claims.Email;
                })
            .AddEntityFrameworkStores<IdentityUsersContext>()
            .AddDefaultTokenProviders();

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
            .AddScoped<IRegisterManager, RegisterManager>();

        return services;
    }
}
