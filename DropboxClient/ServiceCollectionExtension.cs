using Microsoft.Extensions.DependencyInjection;

namespace DropboxClient
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDropboxConnector(this IServiceCollection services)
        {
            return services;
        }
    }
}