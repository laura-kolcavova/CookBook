using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.HostedServices;
using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services;
using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenIddictServer(
       this IServiceCollection services)
    {
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

                    options
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.RegisterScopes(
                        Scopes.OpenId,
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

        services
            .AddTransient<IOpenIddictServerSeeder, OpenIddictServerSeeder>();

        services
            .AddHostedService<OpenIddictServerSeedingWorker>();

        return services;
    }
}
