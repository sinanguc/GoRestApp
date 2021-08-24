using GorestApp.DataAccess.Abstract.Repositories;
using GorestApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GorestApp.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    {
        private readonly GorestAppContext _gorestAppContext;
        private readonly DbSet<TEntity> _entities;

        public EfEntityRepositoryBase(GorestAppContext gorestAppContext)
        {
            _gorestAppContext = gorestAppContext;
            _entities = gorestAppContext.Set<TEntity>();
        }

        #region Queries

        #region Sync Functions
        public virtual TEntity GetById(params object[] id)
        {
            return _entities.Find(id);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return _entities.AsNoTracking().FirstOrDefault(filter);
        }

        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return _entities.AsNoTracking().LastOrDefault(filter);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> condition)
        {
            return _entities.Any(condition);
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? _entities.Where(predicate).ToList() : _entities.ToList();
        }
        #endregion

        #region Async Functions
        public async virtual Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await _entities.FindAsync(id).ConfigureAwait(false);
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(filter).ConfigureAwait(false);
        }
        public virtual async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _entities.AsNoTracking().LastOrDefaultAsync(filter).ConfigureAwait(false);
        }
        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _entities.AnyAsync(condition).ConfigureAwait(false);
        }
        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? await _entities.Where(predicate).ToListAsync() : await _entities.ToListAsync();
        }
        #endregion

        #endregion

        #region Commands
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity is ISoftDeleteEntity softSilindiEntity)
                softSilindiEntity.Deleted = false;

            var addedEntity = _entities.Add(entity);
            _gorestAppContext.Entry(entity).State = EntityState.Added;
            return addedEntity.Entity;
        }
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _gorestAppContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));
                case ISoftDeleteEntity softSilindiEntity:
                    softSilindiEntity.Deleted = true;
                    _entities.Attach(entity).State = EntityState.Modified;
                    break;
                default:
                    _entities.Remove(entity);
                    break;
            }
        }
        #endregion
    }
}
