using GorestApp.DataAccess.Abstract.Repositories;
using GorestApp.Entities.Concrete.Users;
using System.Threading.Tasks;

namespace GorestApp.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        public GorestAppContext GorestAppContext { get; }
        public IEntityRepository<User> UserRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
