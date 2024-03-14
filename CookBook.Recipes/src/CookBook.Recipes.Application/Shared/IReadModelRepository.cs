using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Domain.Shared;
using System.Linq.Expressions;

namespace CookBook.Recipes.Application.Common;

public interface IReadModelRepository<TReadModel>
    where TReadModel : class, IReadModel
{
    Task<TReadModel?> GetOneAsync(
       Expression<Func<TReadModel, bool>> filter,
       CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TReadModel>> GetAllAsync(
       IReadOnlyCollection<SortBy>? sorting = null,
       OffsetFilter? offsetFilter = null,
       CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TReadModel>> GetManyAsync(
        Expression<Func<TReadModel, bool>> filter,
        OffsetFilter? offsetFilter = null,
        IReadOnlyCollection<SortBy>? sorting = null,
        CancellationToken cancellationToken = default);
}
