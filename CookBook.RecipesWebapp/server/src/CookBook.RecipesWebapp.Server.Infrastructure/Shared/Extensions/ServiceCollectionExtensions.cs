using CookBook.RecipesWebapp.Server.Infrastructure.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        bool isDevelopment)
    {
        //services
        //    .AddAuthentication(
        //        options =>
        //        {
        //            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //        })
        //    .AddCookie(
        //        options =>
        //        {
        //            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        //        });

        services
            .AddUsers();

        return services;
    }
}
