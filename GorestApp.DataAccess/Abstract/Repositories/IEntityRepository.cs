using GorestApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GorestApp.DataAccess.Abstract.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        #region Queries

        #region Sync Functions
        TEntity GetById(params object[] id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null);
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> filter = null);
        bool Any(Expression<Func<TEntity, bool>> condition);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        #endregion


        #region Async Functions
        Task<TEntity> GetByIdAsync(params object[] id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #endregion

        #region Commands
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        #endregion
    }
}
