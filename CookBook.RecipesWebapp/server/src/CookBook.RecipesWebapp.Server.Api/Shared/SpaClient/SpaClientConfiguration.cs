namespace CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;

public sealed class SpaClientConfiguration
{
    public bool IsSpaEnabled { get; init; }

    public string StaticFilesRootPath { get; init; } = string.Empty;

    public bool UseDevelopmentProxyServer { get; init; }

    public string DevelopmentProxyServerBaseUri { get; init; } = string.Empty;
}
