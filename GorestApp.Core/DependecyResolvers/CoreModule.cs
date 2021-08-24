using GorestApp.Core.DependecyResolvers.Interfaces;
using GorestApp.Core.Infrastructure.Caching.Microsoft;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace GorestApp.Core.DependecyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
