using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Domain.Common;
using System.Linq.Expressions;

namespace CookBook.Recipes.Application.Common;

public interface IReadModelRepository<TReadModel, TPrimaryKey>
    where TReadModel : class, IReadModel<TPrimaryKey>
{
    ValueTask<TReadModel?> GetOneAsync(
        TPrimaryKey primaryKey,
        CancellationToken cancellationToken = default);

    Task<TReadModel?> GetOneAsync(
        Expression<Func<TReadModel, bool>> filter,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TReadModel>> GetManyAsync(
        Expression<Func<TReadModel, bool>> filter,
        OffsetFilter? offsetFilter = null,
        IReadOnlyCollection<SortBy>? sorting = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TReadModel>> GetAllAsync(
        IReadOnlyCollection<SortBy>? sorting = null,
        CancellationToken cancellationToken = default);
}
