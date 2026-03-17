using System.ComponentModel.DataAnnotations;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi;

internal sealed class RecipesApiConfiguration
{
    [Required]
    public string BaseAddress { get; set; } = string.Empty;
}
