using System.ComponentModel.DataAnnotations;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi;

public sealed class IdentityProviderApiConfiguration
{
    [Required]
    public string BaseAddress { get; set; } = string.Empty;
}
