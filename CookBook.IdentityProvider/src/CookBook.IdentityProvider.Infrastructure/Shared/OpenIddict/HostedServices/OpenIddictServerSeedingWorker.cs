using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.HostedServices;

internal sealed class OpenIddictServerSeedingWorker(
    IServiceProvider serviceProvider) :
    IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using (var scope = serviceProvider.CreateAsyncScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IOpenIddictServerSeeder>();

            await seeder.SeedApplications(
                cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
