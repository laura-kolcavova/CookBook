using Carter;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Yarp.TransformProviders;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
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
                    options.HeaderName = ConfigurationConstants.Antiforgery.RequestVerificationTokenHeaderName;
                    options.Cookie.Name = ConfigurationConstants.Antiforgery.TokenCookieName;
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
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;

                    options.Cookie.Name = ConfigurationConstants.Identity.CookieName;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                    options.Events.OnRedirectToLogin = redirectContext =>
                    {
                        return redirectContext
                            .HttpContext
                            .WriteProblemDetails(StatusCodes.Status401Unauthorized);
                    };

                    options.Events.OnRedirectToAccessDenied = redirectContext =>
                    {
                        return redirectContext
                            .HttpContext
                            .WriteProblemDetails(StatusCodes.Status403Forbidden);
                    };
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
                    options.Scope.Add(OpenIddictConstants.Scopes.Roles);
                    options.Scope.Add(openIdConnectAppConfiguration.Scopes.CookBookRecipesReadWrite);

                    //if (isDevelopment)
                    //{
                    //    options.RequireHttpsMetadata = false;
                    //}

                    options.RequireHttpsMetadata = false;

                    options.SaveTokens = true;
                    options.MapInboundClaims = false;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

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
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = applicationName,
                    Version = "v1"
                });

                options.SupportNonNullableReferenceTypes();

                options.CustomSchemaIds(x => x.FullName?
                    .Replace("Dto", string.Empty)
                    .Replace("+", "."));
            });

        services
            .AddProblemDetails();

        services
            .AddCarter(
                new DependencyContextAssemblyCatalog(
                    [typeof(Program).Assembly]));

        services
            .AddReverseProxy()
            .LoadFromConfig(reverseProxyConfiguration)
            .AddTransforms<AntiforgeryValidationTransformProvider>()
            .AddTransforms<OpenIdConnectTransformProvider>();

        services
            .AddValidatorsFromAssembly(
                typeof(Program).Assembly,
                ServiceLifetime.Singleton,
                includeInternalTypes: true);

        return services;
    }
}
