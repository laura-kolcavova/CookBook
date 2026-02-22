namespace CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpaClient(
        this IServiceCollection services,
        SpaClientConfiguration spaClientConfiguration)
    {
        services.AddSpaStaticFiles(c =>
        {
            c.RootPath = spaClientConfiguration.StaticFilesRootPath;
        });

        return services;
    }
}
