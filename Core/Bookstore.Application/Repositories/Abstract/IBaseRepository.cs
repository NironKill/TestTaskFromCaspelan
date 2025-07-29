using System.Linq.Expressions;

namespace Bookstore.Application.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TCommand, TQuery>
    {
        Task<Guid> Create(TCommand dto, Func<TCommand, TEntity> map, CancellationToken cancellationToken);
        Task<bool> Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TQuery> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, Func<TEntity, TQuery> map, CancellationToken cancellationToken);

        Task<TQuery> Get(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TQuery> map, CancellationToken cancellationToken);
        Task<ICollection<TQuery>> GetAll(Func<TEntity, TQuery> map, CancellationToken cancellationToken);
        Task<ICollection<TQuery>> GetAll(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TQuery> map, CancellationToken cancellationToken);
    }
}
