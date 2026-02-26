namespace CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;

public interface IOpenIddictServerSeeder
{
    public Task SeedApplications(
        CancellationToken cancellationToken);
}
