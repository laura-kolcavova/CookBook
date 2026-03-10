using System.ComponentModel.DataAnnotations;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIdConnect;

public sealed class OpenIdConnectAppConfiguration
{
    [Required]
    public string Authority { get; set; } = string.Empty;

    [Required]
    public string ClientId { get; set; } = string.Empty;

    [Required]
    public string ClientSecret { get; set; } = string.Empty;

    public OpenIdConnectScopes Scopes { get; set; } = new();

    public sealed record OpenIdConnectScopes
    {
        public string CookBookRecipesReadWrite { get; set; } = string.Empty;
    }
}
