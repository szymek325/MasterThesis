using DataLayer;
using DropboxClient;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddDropboxConnector();
            services.AddDataLayerModule();
            return services;
        }
    }
}