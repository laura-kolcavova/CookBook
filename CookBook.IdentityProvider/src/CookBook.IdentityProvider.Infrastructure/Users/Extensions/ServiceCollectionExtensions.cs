using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Infrastructure.Shared.Interceptors;
using CookBook.IdentityProvider.Infrastructure.Users.UseCases.RegisterUser;
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

                    if (isDevelopment)
                    {
                        options
                            .AddDevelopmentEncryptionCertificate()
                            .AddDevelopmentSigningCertificate();
                    }

                    options.RegisterScopes(
                        Scopes.Email,
                        Scopes.Profile);

                    options
                        .UseAspNetCore()
                        .DisableTransportSecurityRequirement()
                        .EnableTokenEndpointPassthrough();
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
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        return services;
    }
}
