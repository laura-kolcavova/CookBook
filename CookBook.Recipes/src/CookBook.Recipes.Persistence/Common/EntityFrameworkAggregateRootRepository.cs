//using CookBook.Recipes.Application.Common;
//using CookBook.Recipes.Application.Common.Filtering;
//using CookBook.Recipes.Application.Common.Sorting;
//using CookBook.Recipes.Domain.Common;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace CookBook.Recipes.Persistence.Common;

//internal class EntityFrameworkAggregateRootRepository<TDbContext, TAggregateRoot, TPrimaryKey> : IAggregateRootRepository<TAggregateRoot, TPrimaryKey>
//    where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>
//    where TDbContext : DbContext
//{
//    private readonly TDbContext _dbContext;
//    private readonly DbSet<TAggregateRoot> _aggregateRootSet;

//    public EntityFrameworkAggregateRootRepository(TDbContext dbContext)
//    {
//        _aggregateRootSet = dbContext.Set<TAggregateRoot>();
//        _dbContext = dbContext;
//    }

//    public async ValueTask AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
//    {
//        await _aggregateRootSet
//            .AddAsync(aggregateRoot, cancellationToken);
//    }

//    public async Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> filter, CancellationToken cancellationToken = default)
//    {
//        return await _aggregateRootSet
//            .AnyAsync(filter, cancellationToken);
//    }

//    public Task<bool> ExistsAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public async Task<IEnumerable<TAggregateRoot>> GetAllAsync(IReadOnlyCollection<SortBy>? sorting = null, CancellationToken cancellationToken = default)
//    {
//        return await _aggregateRootSet
//            .ToListAsync(cancellationToken);
//    }

//    public async Task<IEnumerable<TAggregateRoot>> GetManyAsync(Expression<Func<TAggregateRoot, bool>> filter, OffsetFilter? offsetFilter = null, IReadOnlyCollection<SortBy>? sorting = null, CancellationToken cancellationToken = default)
//    {
//        return await _aggregateRootSet
//            .Where(filter)
//            .ToListAsync(cancellationToken);
//    }

//    public async ValueTask<TAggregateRoot?> GetOneAsync(TPrimaryKey primaryKey, CancellationToken cancellationToken = default)
//    {
//        return await _aggregateRootSet
//            .FindAsync(primaryKey, cancellationToken);
//    }

//    public async Task<TAggregateRoot?> GetOneAsync(Expression<Func<TAggregateRoot, bool>> filter, CancellationToken cancellationToken = default)
//    {
//        return await _aggregateRootSet
//            .SingleOrDefaultAsync(filter, cancellationToken);
//    }

//    public void Remove(TAggregateRoot aggregateRoot)
//    {
//        _aggregateRootSet.Remove(aggregateRoot);
//    }

//    public void Remove(TPrimaryKey id)
//    {
//        throw new NotImplementedException();
//    }

//    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//    {
//        return await _dbContext
//            .SaveChangesAsync(cancellationToken);
//    }

//    public void Update(TAggregateRoot aggregateRoot)
//    {
//        _aggregateRootSet
//            .Update(aggregateRoot);
//    }
//}
