using Carter;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
using System.Text.Json.Serialization;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        string applicationName)
    {
        services
            .AddAntiforgery(options =>
            {
                options.FormFieldName = ConfigurationConstants.Antiforgery.RequestVerificationTokenFormFieldName;
                options.Cookie.Name = ConfigurationConstants.Antiforgery.TokenCookieName;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

        //services
        //   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //   .AddJwtBearer(
        //       options =>
        //       {
        //           options.Authority = openIdConnectAppConfiguration.Authority;

        //           options.MapInboundClaims = false;

        //           //if (isDevelopment)
        //           //{
        //           //    options.RequireHttpsMetadata = false;
        //           //}

        //           options.RequireHttpsMetadata = false;

        //           options.TokenValidationParameters = new TokenValidationParameters
        //           {
        //               ValidateIssuer = true,
        //               ValidateAudience = false,
        //               ValidIssuers = openIdConnectAppConfiguration.Issuers,
        //               ValidTypes = [
        //                   "at+jwt"
        //               ]
        //           };
        //       });

        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                ConfigurationConstants.AuthenticationPolicies.ReadWrite,
                builder =>
                {
                    builder
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .RequireAssertion(context => context.User.HasScope(ConfigurationConstants.AuthenticationScopes.CookBookIdentityProviderReadWrite));
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
            .AddLocalization(
                options =>
                {
                    options.ResourcesPath = "Resources";
                });

        services
            .AddRazorPages(
                options =>
                {
                    options.Conventions.AddPageRoute("/home/index", "");
                })
            .AddViewLocalization();

        return services;
    }
}
