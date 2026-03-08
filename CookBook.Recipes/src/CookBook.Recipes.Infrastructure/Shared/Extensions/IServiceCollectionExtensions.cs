using CookBook.Recipes.Infrastructure.Recipes.Extensions;
using CookBook.Recipes.Infrastructure.Shared.Interceptors;
using CookBook.Recipes.Infrastructure.Shared.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Infrastructure.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment,
        OpenIdConnectAppConfiguration openIdConnectAppConfiguration)
    {
        services
            .AddOpenIddict()
            .AddValidation(
                options =>
                {
                    options.SetIssuer(openIdConnectAppConfiguration.Authority);
                    options.AddAudiences("CookBook.Recipes");

                    //options.UseIntrospection()
                    //       .SetClientId("CookBook.Recipes")
                    //       .SetClientSecret("");

                    options.UseSystemNetHttp();

                    options.UseAspNetCore();
                });

        services
           .AddHealthChecks()
           .AddSqlServer(
               connectionString,
               name: "CookBookRecipes_DB",
               tags: new[]
               {
                    "readiness"
               });

        services
          .AddSingleton<UpdateTrackingFieldsInterceptor>();

        services
            .AddRecipes(
                connectionString,
                isDevelopment);

        return services;
    }
}
