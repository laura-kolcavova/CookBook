using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Shared.Interceptors;
using CookBook.IdentityProvider.Infrastructure.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
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

        services
            .AddQuartz(options =>
            {
                options.UseSimpleTypeLoader();
                options.UseInMemoryStore();
            });

        services
            .AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services
            .AddOpenIddict()
            .AddCore(
                options =>
                {
                    options
                        .UseEntityFrameworkCore()
                        .UseDbContext<IdentityUsersContext>()
                        .ReplaceDefaultEntities<int>();

                    options
                        .UseQuartz();
                })
            .AddServer(
                options =>
                {
                    options.SetTokenEndpointUris(
                        "api/authorization/token");

                    options.SetUserInfoEndpointUris(
                        "api/authorization/userinfo");

                    options.AllowPasswordFlow();


                    options.AcceptAnonymousClients();

                    options
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.RegisterScopes(
                        Scopes.Email,
                        Scopes.Profile);

                    options
                        .UseAspNetCore()
                        .DisableTransportSecurityRequirement()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserInfoEndpointPassthrough();
                })
        .AddValidation(
            options =>
            {
                options.UseLocalServer();

                options.UseAspNetCore();
            });

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
