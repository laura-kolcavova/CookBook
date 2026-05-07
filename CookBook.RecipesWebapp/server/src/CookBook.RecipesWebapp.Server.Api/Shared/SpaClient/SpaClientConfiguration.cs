namespace CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;

public sealed class SpaClientConfiguration
{
    public bool IsSpaEnabled { get; set; }

    public string StaticFilesRootPath { get; set; } = string.Empty;

    public bool UseDevelopmentProxyServer { get; set; }

    public string DevelopmentProxyServerBaseUri { get; set; } = string.Empty;
}
