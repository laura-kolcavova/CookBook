using Carter;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect.Transformers;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
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
          .AddTransforms<OpenIdConnectClientTransformer>();

        services.AddHttpForwarder();

        //    .AddTransforms(builder =>
        //    {
        //        builder.AddRequestTransform(async context =>
        //        {
        //            var result = await context
        //                .HttpContext
        //                .AuthenticateAsync(
        //                    CookieAuthenticationDefaults.AuthenticationScheme);

        //            if (result is not { Succeeded: true })
        //            {
        //                return;
        //            }

        //            var proxyRequestOptions = context
        //                .ProxyRequest
        //                .Options;

        //            proxyRequestOptions.Set(
        //                key: new(
        //                    OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken),
        //                value: result
        //                    .Properties
        //                    .GetTokenValue(
        //                        OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken));

        //            proxyRequestOptions.Set(
        //                key: new(
        //                    OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessTokenExpirationDate),
        //                value: result
        //                    .Properties
        //                    .GetTokenValue(
        //                        OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessTokenExpirationDate));

        //            proxyRequestOptions.Set(
        //                key: new(
        //                    OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken),
        //                value: result
        //                    .Properties
        //                    .GetTokenValue(
        //                        OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken));
        //        });

        //        builder.AddResponseTransform(async context =>
        //        {
        //            if (context.ProxyResponse is not TokenRefreshingHttpResponseMessage response)
        //            {
        //                return;
        //            }

        //            var result = await context
        //                .HttpContext
        //                .AuthenticateAsync(
        //                    CookieAuthenticationDefaults.AuthenticationScheme);

        //            if (result is not { Succeeded: true })
        //            {
        //                return;
        //            }

        //            var properties = result.Properties.Clone();

        //            properties.UpdateTokenValue(
        //                OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken,
        //                response.RefreshTokenAuthenticationResult.AccessToken);

        //            properties.UpdateTokenValue(
        //                OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessTokenExpirationDate,
        //                response
        //                    .RefreshTokenAuthenticationResult
        //                    .AccessTokenExpirationDate
        //                    ?.ToString(CultureInfo.InvariantCulture)
        //                    ?? string.Empty);

        //            // Note: if no refresh token was returned, preserve the refresh token initially returned.
        //            if (!string.IsNullOrEmpty(response.RefreshTokenAuthenticationResult.RefreshToken))
        //            {
        //                properties.UpdateTokenValue(
        //                    OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken,
        //                    response
        //                        .RefreshTokenAuthenticationResult
        //                        .RefreshToken);
        //            }

        //            properties.RedirectUri = null;

        //            properties.IssuedUtc = TimeProvider.System.GetUtcNow();
        //            properties.ExpiresUtc = properties.IssuedUtc + TimeSpan.FromDays(7);

        //            await context
        //                .HttpContext
        //                .SignInAsync(
        //                    result.Ticket.AuthenticationScheme,
        //                    result.Principal,
        //                    properties);
        //        });
        //    });

        //services.Replace(
        //    ServiceDescriptor.Singleton<
        //        IForwarderHttpClientFactory,
        //        TokenRefreshingForwarderHttpClientFactory>());

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
