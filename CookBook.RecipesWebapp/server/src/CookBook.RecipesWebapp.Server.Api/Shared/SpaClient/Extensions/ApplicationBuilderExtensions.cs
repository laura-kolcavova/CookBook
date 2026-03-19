using CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.Extensions;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace CookBook.RecipesWebapp.Server.Api.Shared.SpaClient.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSpaClient(
        this IApplicationBuilder app,
        IConfigurationManager configuration)
    {
        var spaClientConfiguration = configuration
            .GetRequiredSection(nameof(SpaClientConfiguration))
            .Get<SpaClientConfiguration>()!;

        if (!spaClientConfiguration.IsSpaEnabled)
        {
            return app;
        }

        app.MapWhen(
            ctx =>
                !ctx.Request.IsApiRequest() &&
                !ctx.Request.IsLessKnownRequest(),
            spaAppBuilder =>
            {
                spaAppBuilder.UseWhen(
                    ctx => ctx.Request.IsRenderingRequest(),
                    appBuilder =>
                    {
                        appBuilder.UseAntiforgeryTokens();
                    });

                var useStaticFiles = !spaClientConfiguration.UseDevelopmentProxyServer;

                if (useStaticFiles)
                {
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(
                                Directory.GetCurrentDirectory(),
                                spaClientConfiguration.StaticFilesRootPath),
                            ExclusionFilters.None)
                    });
                }

                spaAppBuilder.UseSpa(spa =>
                {
                    if (spaClientConfiguration.UseDevelopmentProxyServer)
                    {
                        spa.UseProxyToSpaDevelopmentServer(
                            spaClientConfiguration.DevelopmentProxyServerBaseUri);
                    }
                });
            });

        return app;
    }
}
