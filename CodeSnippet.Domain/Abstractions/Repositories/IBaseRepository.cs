using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSnippet.Domain.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> Get(int start, int limit, string? orderBy, string? order, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default);
        Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> Find(Guid id, CancellationToken cancellationToken = default);
        Task Add(TEntity entity, CancellationToken cancellationToken = default);
        Task Update(Expression<Func<TEntity, bool>> predicate, TEntity entity, CancellationToken cancellationToken = default);
        Task Update(TEntity entity, CancellationToken cancellationToken = default);
        Task Delete(TEntity entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> LoadReference(Expression<Func<TEntity, string>> refs, CancellationToken cancellationToken = default);
        Task<int> Save(CancellationToken cancellationToken = default);
    }
}
