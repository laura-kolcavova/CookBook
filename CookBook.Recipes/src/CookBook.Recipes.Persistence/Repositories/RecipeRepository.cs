using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Application.Repositories;
using CookBook.Recipes.Domain.Entities.Recipes;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CookBook.Recipes.Persistence.Repositories;

internal class RecipeRepository : IRecipeRepository
{
    private readonly RecipesContext _recipesContext;
    private readonly DbSet<RecipeAggregate> _dbSet;

    public RecipeRepository(RecipesContext recipesContext)
    {
        _recipesContext = recipesContext;
        _dbSet = _recipesContext.Set<RecipeAggregate>();
    }

    public async ValueTask AddAsync(RecipeAggregate aggregateRoot, CancellationToken cancellationToken = default)
    {
        await _dbSet
            .AddAsync(aggregateRoot, cancellationToken);
    }

    public async Task<bool> ExistsAsync(long primaryKey, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(recipe => recipe.Id == primaryKey, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<RecipeAggregate, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(filter, cancellationToken);
    }

    public async Task<IEnumerable<RecipeAggregate>> GetAllAsync(IReadOnlyCollection<SortBy>? sorting = null, CancellationToken cancellationToken = default)
    {
        var queryable = _dbSet
            .IncludeAll();

        if (sorting is not null)
        {
            queryable.SortBy(sorting);
        }

        return await queryable
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RecipeAggregate>> GetManyAsync(Expression<Func<RecipeAggregate, bool>> filter, OffsetFilter? offsetFilter = null, IReadOnlyCollection<SortBy>? sorting = null, CancellationToken cancellationToken = default)
    {
        var queryable = _dbSet
            .IncludeAll()
            .Where(filter);

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

    public async ValueTask<RecipeAggregate?> GetOneAsync(long primaryKey, CancellationToken cancellationToken = default)
    {
        var recipe = await _dbSet
            .FindAsync(new object?[] { primaryKey, cancellationToken }, cancellationToken: cancellationToken);

        if (recipe is not null)
        {
            await _dbSet
                .Entry(recipe)
                .Collection(recipe => recipe.Ingredients)
                .LoadAsync(cancellationToken);

            await _dbSet
                .Entry(recipe)
                .Collection(recipe => recipe.Instructions)
                .LoadAsync(cancellationToken);
        }

        return recipe;
    }

    public async Task<RecipeAggregate?> GetOneAsync(Expression<Func<RecipeAggregate, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .IncludeAll()
            .SingleOrDefaultAsync(filter, cancellationToken);
    }

    public async Task ExecuteRemoveAsync(long primaryKey, CancellationToken cancellationToken)
    {
        await _dbSet
            .Where(recipe => recipe.Id == primaryKey)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public void Remove(RecipeAggregate aggregateRoot)
    {
        _dbSet
            .Remove(aggregateRoot);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _recipesContext
            .SaveChangesAsync(cancellationToken);
    }

    public void Update(RecipeAggregate aggregateRoot)
    {
        _dbSet
            .Update(aggregateRoot);
    }
}
