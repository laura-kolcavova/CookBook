using Carter;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using Microsoft.OpenApi.Models;
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
            .AddAuthorization();

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
            .AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/home/index", "");
            });

        return services;
    }
}
