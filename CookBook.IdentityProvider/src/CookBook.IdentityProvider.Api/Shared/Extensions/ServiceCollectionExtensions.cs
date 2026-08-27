using Carter;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using FluentValidation;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
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

        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                ConfigurationConstants.AuthenticationPolicies.ReadWrite,
                builder =>
                {
                    builder
                        .AddAuthenticationSchemes(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
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
            .AddValidatorsFromAssembly(
                typeof(Program).Assembly,
                ServiceLifetime.Singleton,
                includeInternalTypes: true);

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
