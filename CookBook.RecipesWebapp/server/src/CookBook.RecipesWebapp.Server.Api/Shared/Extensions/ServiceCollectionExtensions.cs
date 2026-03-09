using Carter;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect.Yarp.TransformProviders;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        string applicationName,
        bool isDevelopment,
        IConfigurationSection reverseProxyConfiguration,
        OpenIdConnectAppConfiguration openIdConnectAppConfiguration)
    {
        services
            .AddAntiforgery(
                options =>
                {
                    options.HeaderName = ConfigurationConstants.Antiforgery.HeaderName;
                    options.Cookie.Name = ConfigurationConstants.Antiforgery.CookieName;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                });

        services
            .AddAuthentication(
                options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
            .AddCookie(
                options =>
                {
                    //options.LoginPath = "/login";
                    //options.LogoutPath = "/logout";

                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;

                    options.Cookie.Name = ConfigurationConstants.Identity.CookieName;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                })
            .AddOpenIdConnect(
                options =>
                {
                    options.Authority = openIdConnectAppConfiguration.Authority;
                    options.ClientId = openIdConnectAppConfiguration.ClientId;
                    options.ClientSecret = openIdConnectAppConfiguration.ClientSecret;

                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    options.Scope.Add(OpenIddictConstants.Scopes.OpenId);
                    options.Scope.Add(OpenIddictConstants.Scopes.Email);
                    options.Scope.Add(OpenIddictConstants.Scopes.Profile);

                    if (isDevelopment)
                    {
                        options.RequireHttpsMetadata = false;
                    }

                    options.SaveTokens = true;
                    options.MapInboundClaims = false;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

        //services
        //    .AddOpenIddict()
        //    .AddClient(
        //        options =>
        //        {
        //            options
        //                .AllowAuthorizationCodeFlow()
        //                .AllowRefreshTokenFlow();

        //            options
        //                .AddDevelopmentEncryptionCertificate()
        //                .AddDevelopmentSigningCertificate();

        //            var aspNetCoreBuilder = options
        //                .UseAspNetCore()
        //                .EnableStatusCodePagesIntegration()
        //                .EnableRedirectionEndpointPassthrough();

        //            if (isDevelopment)
        //            {
        //                aspNetCoreBuilder.DisableTransportSecurityRequirement();
        //            }

        //            options
        //                .UseSystemNetHttp()
        //                .SetProductInformation(typeof(Program).Assembly);

        //            options.DisableTokenStorage();

        //            options.AddRegistration(
        //                new OpenIddictClientRegistration
        //                {
        //                    Issuer = new Uri(
        //                        openIdConnectAppConfiguration.Authority,
        //                        UriKind.Absolute),

        //                    ClientId = openIdConnectAppConfiguration.ClientId,
        //                    ClientSecret = openIdConnectAppConfiguration.ClientSecret,

        //                    Scopes = {
        //                        OpenIddictConstants.Scopes.OpenId,
        //                        OpenIddictConstants.Scopes.Email,
        //                        OpenIddictConstants.Scopes.Profile
        //                    },

        //                    RedirectUri = new Uri(
        //                        "/signin-oidc",
        //                        UriKind.Relative)
        //                });
        //        });

        services.AddAuthorizationBuilder()
            .AddPolicy(
                ConfigurationConstants.AuthenticationPolicies.Cookie,
                builder =>
                {
                    builder.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                    builder.RequireAuthenticatedUser();
                });

        services
          .ConfigureHttpJsonOptions(options =>
          {
              options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
          });

        services
            .AddCarter(
                new DependencyContextAssemblyCatalog(
                    [typeof(Program).Assembly]));

        services
          .AddReverseProxy()
          .LoadFromConfig(reverseProxyConfiguration)
          .AddTransforms<OpenIdConnectTransformProvider>();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerExamplesFromAssemblyOf<Program>()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = applicationName,
                    Version = "v1"
                });

                options.ExampleFilters();
                options.SupportNonNullableReferenceTypes();

                options.CustomSchemaIds(x => x.FullName?
                    .Replace("Dto", string.Empty)
                    .Replace("+", "."));
            });

        services
            .AddProblemDetails();

        services
            .AddValidatorsFromAssembly(
                typeof(Program).Assembly,
                ServiceLifetime.Singleton,
                includeInternalTypes: true);

        return services;
    }
}
