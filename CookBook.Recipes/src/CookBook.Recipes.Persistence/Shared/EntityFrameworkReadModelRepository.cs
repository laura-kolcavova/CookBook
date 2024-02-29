using CookBook.Recipes.Application.Common;
using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Domain.Common;
using CookBook.Recipes.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CookBook.Recipes.Persistence.Common;

internal class EntityFrameworkReadModelRepository<TDbContext, TReadModel, TPrimaryKey> :
    IReadModelRepository<TReadModel, TPrimaryKey>
    where TReadModel : class, IReadModel<TPrimaryKey>
    where TDbContext : DbContext
{
    private readonly DbSet<TReadModel> _readModelSet;

    public EntityFrameworkReadModelRepository(TDbContext dbContext)
    {
        _readModelSet = dbContext.Set<TReadModel>();
    }

    public async Task<IReadOnlyCollection<TReadModel>> GetAllAsync(
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken = default)
    {
        var queryable = _readModelSet
            .AsNoTracking();

        if (offsetFilter is not null)
        {
            queryable = queryable
                .Skip(offsetFilter.Offset)
                .Take(offsetFilter.Limit);
        }

        if (sorting is not null)
        {
            queryable.SortBy(sorting);
        }

        return await queryable
             .ToListAsync(cancellationToken);
    }

    public async ValueTask<TReadModel?> GetOneAsync(
        TPrimaryKey primaryKey,
        CancellationToken cancellationToken = default)
    {
        return await _readModelSet
            .FindAsync(primaryKey, cancellationToken);
    }

    public async Task<TReadModel?> GetOneAsync(
        Expression<Func<TReadModel, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await _readModelSet
            .AsNoTracking()
            .SingleOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TReadModel>> GetManyAsync(
        Expression<Func<TReadModel, bool>> filter,
        OffsetFilter? offsetFilter = null,
        IReadOnlyCollection<SortBy>? sorting = null,
        CancellationToken cancellationToken = default)
    {
        var queryable = _readModelSet
            .AsNoTracking()
            .Where(filter);

        if (offsetFilter is not null)
        {
            queryable = queryable
                .Skip(offsetFilter.Offset)
                .Take(offsetFilter.Limit);
        }

        if (sorting is not null)
        {
            queryable = queryable
                .SortBy(sorting);
        }

        return await queryable
           .ToListAsync(cancellationToken);
    }
}
