using Bookstore.Application.Interfaces;
using Bookstore.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Application.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TCommand, TQuery> : IBaseRepository<TEntity, TCommand, TQuery>
        where TEntity : BaseEntity
    {
        protected readonly IApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(IApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<Guid> Create(TCommand dto, Func<TCommand, TEntity> map, CancellationToken cancellationToken)
        {
            TEntity newEntity = map(dto);

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
        public virtual async Task<TQuery> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, Func<TEntity, TQuery> map, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            update(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return map(entity);
        }

        public virtual async Task<TQuery> Get(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TQuery> map, CancellationToken cancellationToken)
        {
            TEntity entity = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return map(entity);
        }
        public virtual async Task<ICollection<TQuery>> GetAll(Func<TEntity, TQuery> map, CancellationToken cancellationToken)
        {
            List<TEntity> entities = await _dbSet.ToListAsync(cancellationToken);
            return entities.Select(map).ToList();
        }
        public virtual async Task<ICollection<TQuery>> GetAll(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TQuery> map, CancellationToken cancellationToken)
        {
            List<TEntity> entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
            return entities.Select(map).ToList();
        }
    }
}
