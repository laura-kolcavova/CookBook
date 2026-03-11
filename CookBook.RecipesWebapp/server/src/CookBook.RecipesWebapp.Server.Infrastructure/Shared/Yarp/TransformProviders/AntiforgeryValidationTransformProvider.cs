using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Yarp.TransformProviders;

public sealed class AntiforgeryValidationTransformProvider :
    ITransformProvider
{
    private const string UseAntiforgeryValidationMetaDataName = "UseAntiforgeryValidation";

    public void Apply(
        TransformBuilderContext context)
    {
        if (context.Route.Metadata is null)
        {
            return;
        }

        var useAntiforgeryValidation = context
            .Route
            .Metadata
            .TryGetValue(
                UseAntiforgeryValidationMetaDataName,
                out var useOpenIdConnectValue) &&
            !string.IsNullOrEmpty(useOpenIdConnectValue) &&
            string.Equals(
                useOpenIdConnectValue?.ToString(),
                bool.TrueString,
                StringComparison.OrdinalIgnoreCase);

        if (!useAntiforgeryValidation)
        {
            return;
        }

        context.AddRequestTransform(
            async transformContext =>
            {
                var httpContext = transformContext.HttpContext;

                if (
                    httpContext.Request.Method == HttpMethods.Get ||
                    httpContext.Request.Method == HttpMethods.Head ||
                    httpContext.Request.Method == HttpMethods.Options)
                {
                    return;
                }

                var antiforgery = httpContext
                   .RequestServices
                   .GetRequiredService<IAntiforgery>();

                try
                {
                    await antiforgery.ValidateRequestAsync(httpContext);
                }
                catch (AntiforgeryValidationException)
                {
                    await httpContext.WriteProblemDetails(
                        new ProblemDetails
                        {
                            Detail = "Invalid or missing antiforgery token"
                        });
                }
            });
    }

    public void ValidateCluster(
        TransformClusterValidationContext context)
    {
    }

    public void ValidateRoute(
        TransformRouteValidationContext context)
    {
    }
}
