using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataLayerModule(this IServiceCollection services)
        {
            return services;
        }
    }
}