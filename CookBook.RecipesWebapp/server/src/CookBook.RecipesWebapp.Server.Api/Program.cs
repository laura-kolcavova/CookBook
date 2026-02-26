using Carter;
using CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery;
using CookBook.RecipesWebapp.Server.Api.Shared.Authentication;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Api.Shared.ReverseProxy;
using CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;
using CookBook.RecipesWebapp.Server.Application.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIddict;
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

var spaClientConfiguration = configuration
    .GetRequiredSection(nameof(SpaClientConfiguration))
    .Get<SpaClientConfiguration>()!;

var openIddictConfiguration = configuration
    .GetRequiredSection(nameof(OpenIddictConfiguration))
    .Get<OpenIddictConfiguration>()!;

var reverseProxyConfiguration = configuration
    .GetRequiredSection(ReverseProxyConstants.ConfigurationSectionName);

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
        reverseProxyConfiguration)
    .AddSpaClient(spaClientConfiguration);

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
                    Issuer = new Uri(
                        openIddictConfiguration.IssuerUri,
                        UriKind.Absolute),

                    ClientId = "CookBook.WebApp",
                    ClientSecret = "c0741d5c-f119-4b19-be90-08b6bd1084bf",
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

if (spaClientConfiguration.IsSpaEnabled)
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
