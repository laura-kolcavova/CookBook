namespace CookBook.RecipesWebapp.Server.Api.Shared.Configuration;

public sealed class ClientOptions
{
    public bool IsSpaEnabled { get; init; }

    public string StaticFilesRootPath { get; init; } = string.Empty;

    public bool UseDevelopmentProxyServer { get; init; }

    public string DevelopmentProxyServerBaseUri { get; init; } = string.Empty;
}
