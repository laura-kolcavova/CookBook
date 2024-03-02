using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Domain.Common;
using System.Linq.Expressions;

namespace CookBook.Recipes.Application.Common;

public interface IAggregateRootRepository<TAggregateRoot, TPrimaryKey>
    where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>
{
    ValueTask<TAggregateRoot?> GetOneAsync(
       TPrimaryKey primaryKey,
       CancellationToken cancellationToken = default);

    Task<TAggregateRoot?> GetOneAsync(
        Expression<Func<TAggregateRoot, bool>> filter,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TAggregateRoot>> GetManyAsync(
      Expression<Func<TAggregateRoot, bool>> filter,
      OffsetFilter? offsetFilter = null,
      IReadOnlyCollection<SortBy>? sorting = null,
      CancellationToken cancellationToken = default);

    Task<IEnumerable<TAggregateRoot>> GetAllAsync(
        IReadOnlyCollection<SortBy>? sorting = null,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        TPrimaryKey primaryKey,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Expression<Func<TAggregateRoot, bool>> filter,
        CancellationToken cancellationToken = default);

    ValueTask AddAsync(
        TAggregateRoot aggregateRoot,
        CancellationToken cancellationToken = default);

    Task ExecuteRemoveAsync(
       TPrimaryKey primaryKey,
       CancellationToken cancellationToken);

    void Remove(
        TAggregateRoot aggregateRoot);

    void Update(TAggregateRoot aggregateRoot);

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
