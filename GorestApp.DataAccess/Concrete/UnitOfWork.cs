using GorestApp.Core.Infrastructure.ErrorHandling;
using GorestApp.DataAccess.Abstract;
using GorestApp.DataAccess.Abstract.Repositories;
using GorestApp.Entities.Concrete.Users;
using System;
using System.Threading.Tasks;

namespace GorestApp.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public GorestAppContext GorestAppContext { get; private set; }
        public IEntityRepository<User> UserRepository { get; private set; }

        public UnitOfWork(GorestAppContext gorestAppContext, IEntityRepository<User> userRepository)
        {
            GorestAppContext = gorestAppContext;
            UserRepository = userRepository;
        }

        public void SaveChanges()
        {
            int count = GorestAppContext.SaveChanges();
            if (count <= 0)
                throw new DatabaseException("Database bağlantı hatası");
        }

        public async Task SaveChangesAsync()
        {
            int count = await GorestAppContext.SaveChangesAsync();
            if (count <= 0)
                throw new DatabaseException("Database bağlantı hatası");
        }

        #region Disposable
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                GorestAppContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
