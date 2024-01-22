using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeSnippet.Domain.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(int start, int limit, string? orderBy, string? order);
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> Find(Guid id);
        Task Add(TEntity entity);
        Task Update(Expression<Func<TEntity, bool>> predicate, TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<IEnumerable<TEntity>> LoadReference(Expression<Func<TEntity, string>> refs);
        Task<int> Save();
    }
}
