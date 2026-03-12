using System.ComponentModel.DataAnnotations;

namespace CookBook.Recipes.Infrastructure.Shared.OpenIdConnect;

public sealed class OpenIdConnectAppConfiguration
{
    [Required]
    public string Authority { get; set; } = string.Empty;

    [Required]
    public IReadOnlyCollection<string> Issuers { get; set; } = [];

    [Required]
    public string ClientId { get; set; } = string.Empty;

    [Required]
    public string ClientSecret { get; set; } = string.Empty;
}
