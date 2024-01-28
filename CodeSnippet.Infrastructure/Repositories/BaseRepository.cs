using System.Linq.Expressions;
using CodeSnippet.Domain.Abstractions.Repositories;
using CodeSnippet.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippet.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SqlServerDbContext _context;
        protected DbSet<TEntity> Entities;

        public BaseRepository(SqlServerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Entities = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken = default)
            => await Entities.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<IEnumerable<TEntity>> Get(int start, int limit, string? orderBy, string? order, CancellationToken cancellationToken = default)
        {
            var query = Entities.AsQueryable();

            if (start >= 0) query = query.Skip(start);

            if (limit > 0) query = query.Take(limit);

            if (!string.IsNullOrEmpty(orderBy))
            {
                Expression<Func<TEntity, object>>? propertyExpression = PropertyAccessor<TEntity>(orderBy);
                query = order == "desc" ? query.OrderByDescending(propertyExpression) : query.OrderBy(propertyExpression);
            }

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null) query = query.Where(filter);

            return orderBy != null
                ? await orderBy(query).AsNoTracking().ToListAsync(cancellationToken)
                : await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> Find(Guid id, CancellationToken cancellationToken = default)
            => await Entities.FindAsync(new object[] { id }, cancellationToken);

        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await Entities.AddAsync(entity, cancellationToken);
        }

        public async Task Update(Expression<Func<TEntity, bool>> predicate, TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken)
                                ?? throw new Exception("The record was not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Update(entity);
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            Entities.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> LoadReference(Expression<Func<TEntity, string>> refs, CancellationToken cancellationToken = default)
            => await Entities.Include(refs).ToListAsync(cancellationToken);

        public async Task<int> Save(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        private static Expression<Func<TEntity, object>> PropertyAccessor<TEntity>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var property = Expression.Property(parameter, propertyName);
            var cast = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<TEntity, object>>(cast, parameter);

            return lambda;
        }
    }
}
