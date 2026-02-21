using CookBook.Recipes.Domain.Shared.Sorting;
using System.Linq.Dynamic.Core;
using System.Text;

namespace CookBook.Recipes.Infrastructure.Shared.Extensions;

internal static class IQueryableExtensions
{
    public static IQueryable<T> SortBy<T>(
        this IQueryable<T> queryable,
        IEnumerable<SortBy> sorting)
    {
        if (!sorting.Any())
        {
            return queryable;
        }

        var sb = new StringBuilder();

        foreach (var sortByProperty in sorting)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }

            sb.Append(sortByProperty.PropertyName);

            switch (sortByProperty.Direction)
            {
                case SortingDirection.Ascending:
                    sb.Append(" ASC");
                    break;
                case SortingDirection.Descending:
                    sb.Append(" DESC");
                    break;
            }
        }

        return queryable.OrderBy(sb.ToString());
    }
}
