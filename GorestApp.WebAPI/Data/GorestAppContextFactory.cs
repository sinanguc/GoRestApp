using GorestApp.Core.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GorestApp.WebAPI.Data
{
    public class GorestAppContextFactory : IDesignTimeDbContextFactory<GorestAppContext>
    {
        public GorestAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GorestAppContext>();
            optionsBuilder.UseSqlServer(AppConfigurationHelper.GetConnectionString(), b => b.MigrationsAssembly("GorestApp.WebAPI"));

            return new GorestAppContext(optionsBuilder.Options);
        }
    }
}
