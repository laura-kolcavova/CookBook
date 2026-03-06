using Carter;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, string applicationName)
    {
        services
            .AddAntiforgery(options =>
            {
                options.Cookie.Name = ConfigurationConstants.Antiforgery.CookieName;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

        services
            .AddAuthorization();

        //services
        //    .AddCors(options =>
        //        options
        //            .AddPolicy(
        //                ConfigurationConstants.CorsPolicies.Main,
        //                builder => builder
        //                    .WithOrigins()
        //                    .AllowCredentials()
        //                    .AllowAnyHeader()
        //                    .AllowAnyMethod()
        //                    .SetIsOriginAllowed(origin => true)));

        services
            .AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));

        services
            .AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/home/index", "");
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

        return services;
    }
}
