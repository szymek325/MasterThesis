using DataLayer;
using DataLayer.Repositories.Interface;
using Domain.Providers;
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

            services.AddTransient<IFaceRecognitionJobProvider, FaceRecognitionJobProvider>();
            return services;
        }
    }
}