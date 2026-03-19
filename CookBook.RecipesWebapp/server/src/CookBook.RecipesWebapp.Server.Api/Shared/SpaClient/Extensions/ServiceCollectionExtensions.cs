namespace CookBook.RecipesWebapp.Server.Api.Shared.SpaClient.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpaClient(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var spaClientConfiguration = configuration
            .GetRequiredSection(nameof(SpaClientConfiguration))
            .Get<SpaClientConfiguration>()!;

        services.AddSpaStaticFiles(c =>
        {
            c.RootPath = spaClientConfiguration.StaticFilesRootPath;
        });

        return services;
    }
}
