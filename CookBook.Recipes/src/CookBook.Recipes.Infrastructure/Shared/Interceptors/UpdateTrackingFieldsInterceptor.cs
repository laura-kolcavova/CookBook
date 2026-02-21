using CookBook.Recipes.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CookBook.Recipes.Infrastructure.Shared.Interceptors;

internal sealed class UpdateTrackingFieldsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        UpdateTrackingFields(dbContext);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateTrackingFields(
        DbContext dbContext)
    {
        var now = DateTimeOffset.UtcNow;

        var trackableEntities = dbContext
            .ChangeTracker
            .Entries<ITrackableEntity>();

        foreach (var entry in trackableEntities)
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Property(p => p.UpdatedAt).CurrentValue = now;
                    break;

                case EntityState.Added:
                    var currentValue = entry.Property(p => p.CreatedAt).CurrentValue;

                    if (currentValue == null)
                    {
                        entry.Property(p => p.CreatedAt).CurrentValue = now;
                    }

                    break;
            }
        }
    }
}
