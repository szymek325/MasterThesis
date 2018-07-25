using AzureFaceApi.Client;
using DataLayer;
using Domain.Configuration;
using Domain.FaceDetection;
using Domain.FaceRecognition;
using Domain.Files;
using Domain.NeuralNetwork;
using Domain.People;
using Domain.SensorsReading;
using Dropbox.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddDropboxConnector();
            services.AddFaceApiClientModule();
            services.AddDataLayerModule();

            services.AddTransient<IFilesDomainService, FilesDomainService>();
            services.AddTransient<IFaceDetectionService, FaceDetectionService>();
            services.AddTransient<IReadingsProvider, ReadingsProvider>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IGuidProvider, GuidProvider>();
            services.AddTransient<INeuralNetworkService, NeuralNetworkService>();
            services.AddTransient<IFaceRecognitionService, FaceRecognitionService>();
            services.AddTransient<IDetectionResultService, DetectionResultService>();

            return services;
        }
    }
}