using System.ComponentModel.DataAnnotations;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIddict;

public sealed class OpenIdConnectClientConfiguration
{
    [Required]
    public string IssuerUri { get; init; } = string.Empty;
}
