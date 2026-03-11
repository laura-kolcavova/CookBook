using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Yarp.TransformProviders;

public sealed class OpenIdConnectTransformProvider :
    ITransformProvider
{
    private const string UseOpenIdConnectMetaDataName = "UseOpenIdConnect";

    public void Apply(
        TransformBuilderContext context)
    {
        if (context.Route.Metadata is null)
        {
            return;
        }

        var useOpenIdConnect = context
            .Route
            .Metadata
            .TryGetValue(
                UseOpenIdConnectMetaDataName,
                out var useOpenIdConnectValue) &&
            !string.IsNullOrEmpty(useOpenIdConnectValue) &&
            string.Equals(
                useOpenIdConnectValue?.ToString(),
                bool.TrueString,
                StringComparison.OrdinalIgnoreCase);

        if (!useOpenIdConnect)
        {
            return;
        }

        context.AddRequestTransform(
            async transformContext =>
            {
                var accessToken = await transformContext
                    .HttpContext
                    .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

                transformContext
                    .ProxyRequest
                    .Headers
                    .Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        accessToken);
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
