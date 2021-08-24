using Microsoft.Extensions.DependencyInjection;

namespace GorestApp.Core.DependecyResolvers.Interfaces
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
