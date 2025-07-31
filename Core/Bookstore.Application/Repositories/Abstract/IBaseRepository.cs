using System.Linq.Expressions;

namespace Bookstore.Application.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TCommand, TResponse>
    {
        Task<Guid> Create(TCommand command, Func<TCommand, TEntity> map, CancellationToken cancellationToken);
        Task<bool> Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TResponse> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, Func<TEntity, TResponse> map, CancellationToken cancellationToken);

        Task<TResponse> Get(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TResponse> map, CancellationToken cancellationToken);
        Task<ICollection<TResponse>> GetAll(Func<TEntity, TResponse> map, CancellationToken cancellationToken);
        Task<ICollection<TResponse>> GetAll(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TResponse> map, CancellationToken cancellationToken);
    }
}
