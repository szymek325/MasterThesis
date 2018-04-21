using DataLayer;
using Domain.Providers;
using DropboxIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddDropboxConnector();
            services.AddDataLayerModule();

            services.AddTransient<IFaceRecognitionJobProvider, FaceRecognitionJobProvider>();
            return services;
        }
    }
}