using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Api.Shared.Options;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var isDevelopment = builder.Environment.IsDevelopment();

builder.Host
    .UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
    });

var clientOptions = configuration
    .GetRequiredSection(nameof(ClientOptions))
    .Get<ClientOptions>()!;

var reverseProxyOptions = configuration
    .GetRequiredSection("ReverseProxyOptions");

//services.AddAntiforgery(options =>
//{
//    options.HeaderName = ConfigurationConstants.XXSFRHeaderName;
//    options.Cookie.Name = ConfigurationConstants.XXSFRCookieName;
//});

services
    .AddApi(
        builder.Environment.ApplicationName,
        reverseProxyOptions)
    .AddClient(clientOptions);

var app = builder.Build();

if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseRouting();

//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();

if (clientOptions.IsSpaEnabled)
{
    app.MapWhen(ctx =>
        !ctx.Request.IsApiRequest() &&
        !ctx.Request.IsSwaggerRenderingRequest(),
        spaAppBuilder =>
        {
            spaAppBuilder.UseWhen(
                ctx => ctx.Request.IsRenderingRequest(),
                appBuilder =>
                {
                    //appBuilder.UseAntiforgeryMiddleware();
                });

            var useStaticFiles = !clientOptions.UseDevelopmentProxyServer;

            if (useStaticFiles)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), clientOptions.StaticFilesRootPath),
                        ExclusionFilters.None)
                });
            }

            spaAppBuilder.UseSpa(spa =>
            {
                if (clientOptions.UseDevelopmentProxyServer)
                {
                    spa.UseProxyToSpaDevelopmentServer(clientOptions.DevelopmentProxyServerBaseUri);
                }
            });
        });
}

app.MapReverseProxy();

app.UseSwagger(options =>
{
    options.RouteTemplate = ".less-known/api-docs/{documentName}.json";
    options.PreSerializeFilters.Add((swagger, _) =>
        // Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
        swagger.Servers.Clear());
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/.less-known/api-docs/v1.json", "v1");
    options.RoutePrefix = ".less-known/api-docs/ui";
    options.ConfigObject.Filter = string.Empty;
    options.ConfigObject.TryItOutEnabled = true;
});

app.Run();
