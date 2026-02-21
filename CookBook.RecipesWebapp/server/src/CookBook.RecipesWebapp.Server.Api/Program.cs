using Carter;
using CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery;
using CookBook.RecipesWebapp.Server.Api.Shared.Authentication;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;
using CookBook.RecipesWebapp.Server.Application.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using OpenIddict.Client;

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

var spaClientOptions = configuration
    .GetRequiredSection(nameof(SpaClientOptions))
    .Get<SpaClientOptions>()!;

var reverseProxyOptions = configuration
    .GetRequiredSection("ReverseProxyOptions");

services
    .AddOptions();

services
    .AddAntiforgery(options =>
    {
        options.HeaderName = AntiforgeryConstants.HeaderName;
        options.Cookie.Name = AntiforgeryConstants.CookieName;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

services
    .AddAuthentication(
        options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
    .AddCookie(
        options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.SlidingExpiration = true;
            options.Cookie.Name = AuthenticationConstants.Cookies.CookieName;
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        });

services
    .AddApplication()
    .AddInfrastructure()
    .AddApi(
        builder.Environment.ApplicationName,
        reverseProxyOptions)
    .AddSpaClient(spaClientOptions);

services
    .AddOpenIddict()
    .AddClient(
        options =>
        {
            options.AllowPasswordFlow();

            options.DisableTokenStorage();

            options.UseSystemNetHttp(
                ).SetProductInformation(typeof(Program).Assembly);


            options.AddRegistration(
                new OpenIddictClientRegistration
                {
                    Issuer = new Uri("http://localhost:5020/", UriKind.Absolute),
                });

            options.UseAspNetCore();
        });

var app = builder.Build();

if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseAntiforgery();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.MapReverseProxy();

if (spaClientOptions.IsSpaEnabled)
{
    app.MapWhen(ctx =>
        !ctx.Request.IsApiRequest() &&
        !ctx.Request.IsLessKnownRequest(),
        spaAppBuilder =>
        {
            spaAppBuilder.UseWhen(
                ctx => ctx.Request.IsRenderingRequest(),
                appBuilder =>
                {
                });

            var useStaticFiles = !spaClientOptions.UseDevelopmentProxyServer;

            if (useStaticFiles)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),
                            spaClientOptions.StaticFilesRootPath),
                        ExclusionFilters.None)
                });
            }

            spaAppBuilder.UseSpa(spa =>
            {
                if (spaClientOptions.UseDevelopmentProxyServer)
                {
                    spa.UseProxyToSpaDevelopmentServer(
                        spaClientOptions.DevelopmentProxyServerBaseUri);
                }
            });
        });
}

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
