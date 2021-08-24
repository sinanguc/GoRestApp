using GorestApp.Core.DependecyResolvers.Interfaces;
using GorestApp.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace GorestApp.Core.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }
            return ServiceTool.Create(services);
        }
    }
}
