using DataLayer;
using Domain.Configuration;
using Domain.FaceDetection;
using Domain.Files;
using Domain.NeuralNetwork;
using Domain.People;
using Domain.SensorsReading;
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

            services.AddTransient<IFilesDomainService, FilesDomainService>();
            services.AddTransient<IFaceDetectionService, FaceDetectionService>();
            services.AddTransient<IReadingsProvider, ReadingsProvider>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IGuidProvider, GuidProvider>();
            services.AddTransient<INeuralNetworkService, NeuralNetworkService>();

            return services;
        }
    }
}