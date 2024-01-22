using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippet.Infrastructure.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly SqlServerDbContext _context;
    protected DbSet<TEntity> Entities;

    public BaseRepository(SqlServerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Entities = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Get()
        => await Entities.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<TEntity>> Get(int start, int limit, string? orderBy, string? order)
    {
        var query = Entities.AsQueryable();

        if (start >= 0) query = query.Skip(start);

        if (limit > 0) query = query.Take(limit);

        if (!string.IsNullOrEmpty(orderBy))
        {
            var propertyExpression = PropertyAccessor<TEntity>(orderBy);
            query = order == "desc" ? query.OrderByDescending(propertyExpression) : query.OrderBy(propertyExpression);
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = Entities;

        if (filter != null) query = query.Where(filter);

        return orderBy != null
            ? await orderBy(query).AsNoTracking().ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Entities;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) query = orderBy(query);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> Find(Guid id)
        => await Entities.FindAsync(id);


    public async Task Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await Entities.AddAsync(entity);
    }

    public async Task Update(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate)
                            ?? throw new Exception("The record was not found.");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
    }

    public async Task Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _context.Update(entity);
    }

    public async Task Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Entities.Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> LoadReference(Expression<Func<TEntity, string>> refs)
        => await Entities.Include(refs).ToListAsync();

    public async Task<int> Save()
        => await _context.SaveChangesAsync();

    private static Expression<Func<TEntity, object>> PropertyAccessor<TEntity>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = Expression.Property(parameter, propertyName);
        var cast = Expression.Convert(property, typeof(object));
        var lambda = Expression.Lambda<Func<TEntity, object>>(cast, parameter);

        return lambda;
    }
}
