using Microsoft.Extensions.DependencyInjection;

namespace CTAM.Core.Interfaces
{
    public interface IServicesSetup
    {
        void AddModuleServices(IServiceCollection services);
    }
}
