using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.HostedServices;
using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services;
using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using Quartz;

namespace CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenIddictServer(
       this IServiceCollection services,
       bool isDevelopment)
    {
        services
            .AddQuartz(options =>
            {
                options.UseSimpleTypeLoader();
                options.UseInMemoryStore();
            });

        services
            .AddQuartzHostedService(options =>
                options.WaitForJobsToComplete = true);

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
                    options.SetAuthorizationEndpointUris(
                        "connect/authorize");

                    options.SetTokenEndpointUris(
                        "connect/token");

                    options.SetUserInfoEndpointUris(
                        "connect/userinfo");

                    options.SetEndSessionEndpointUris(
                        "connect/logout");

                    options
                        .AllowAuthorizationCodeFlow()
                        .AllowClientCredentialsFlow()
                        .AllowRefreshTokenFlow()
                        .AllowPasswordFlow();

                    options
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.RegisterScopes(
                        OpenIddictConstants.Scopes.OpenId,
                        OpenIddictConstants.Scopes.Email,
                        OpenIddictConstants.Scopes.Profile,
                        OpenIddictConstants.Scopes.Roles);

                    options.DisableAccessTokenEncryption();

                    var aspNetCoreBuilder = options
                        .UseAspNetCore()
                        .EnableAuthorizationEndpointPassthrough()
                        .EnableEndSessionEndpointPassthrough()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserInfoEndpointPassthrough()
                        .EnableStatusCodePagesIntegration();

                    if (isDevelopment)
                    {
                        aspNetCoreBuilder.DisableTransportSecurityRequirement();
                    }
                })
            .AddValidation(
                options =>
                {
                    options.UseLocalServer();

                    options.UseAspNetCore();
                });

        services
            .AddTransient<IOpenIddictServerSeeder, OpenIddictServerSeeder>();

        services
            .AddHostedService<OpenIddictServerSeedingWorker>();

        return services;
    }
}
