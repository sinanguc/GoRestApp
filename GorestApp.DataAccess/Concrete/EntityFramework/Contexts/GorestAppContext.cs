using GorestApp.DataAccess.Configuration.UserConfiguration;
using GorestApp.Entities.Concrete.Users;
using Microsoft.EntityFrameworkCore;

namespace GorestApp
{
    public class GorestAppContext : DbContext
    {
        public GorestAppContext(DbContextOptions<GorestAppContext> options)
    : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
