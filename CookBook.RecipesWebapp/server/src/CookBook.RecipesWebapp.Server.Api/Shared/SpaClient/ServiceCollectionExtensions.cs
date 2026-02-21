namespace CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpaClient(
        this IServiceCollection services,
        SpaClientOptions clientOptions)
    {
        services.AddSpaStaticFiles(c =>
        {
            c.RootPath = clientOptions.StaticFilesRootPath;
        });

        return services;
    }
}
