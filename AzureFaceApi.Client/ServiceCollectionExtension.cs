using Microsoft.Extensions.DependencyInjection;

namespace AzureFaceApi.Client
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddFaceApiClientModule(this IServiceCollection services)
        {
            return services;
        }
    }
}