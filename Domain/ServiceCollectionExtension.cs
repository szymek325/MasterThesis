using DataLayer;
using Domain.Providers;
using DropboxIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Domain
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddDropboxConnector();
            services.AddDataLayerModule();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddTransient<IFaceRecognitionJobProvider, FaceRecognitionJobProvider>();
            return services;
        }
    }
}