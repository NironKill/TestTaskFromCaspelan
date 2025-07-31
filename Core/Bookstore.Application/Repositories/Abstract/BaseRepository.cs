using Bookstore.Application.Interfaces;
using Bookstore.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Application.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TCommand, TResponse> : IBaseRepository<TEntity, TCommand, TResponse>
        where TEntity : BaseEntity
    {
        protected readonly IApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(IApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<Guid> Create(TCommand command, Func<TCommand, TEntity> map, CancellationToken cancellationToken)
        {
            TEntity newEntity = map(command);

            await _dbSet.AddAsync(newEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newEntity.Id;
        }
        public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public virtual async Task<TResponse> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, Func<TEntity, TResponse> map, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            update(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return map(entity);
        }

        public virtual async Task<TResponse> Get(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TResponse> map, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return map(entity);
        }
        public virtual async Task<ICollection<TResponse>> GetAll(Func<TEntity, TResponse> map, CancellationToken cancellationToken)
        {
            List<TEntity> entities = await _dbSet.ToListAsync(cancellationToken);
            return entities.Select(map).ToList();
        }
        public virtual async Task<ICollection<TResponse>> GetAll(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TResponse> map, CancellationToken cancellationToken)
        {
            List<TEntity> entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
            return entities.Select(map).ToList();
        }
    }
}
