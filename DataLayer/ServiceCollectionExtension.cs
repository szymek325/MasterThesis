using System;
using System.Reflection;
using DataLayer.Configuration;
using DataLayer.Repositories.Implementation;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DataLayer
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataLayerModule(this IServiceCollection services)
        {
            services.AddTransient<IDetectionRepository, Repositories.Implementation.DetectionRepository>();
            services.AddTransient<IDetectionResultRepository, DetectionResultRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IRecognitionRepository, RecognitionRepository>();
            services.AddTransient<IRecognitionResultRepository, RecognitionResultRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ISensorsReadingRepository, SensorsReadingRepository>();
            services.AddTransient<INeuralNetworkRepository, NeuralNetworkRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationSettingsRepository, NotificationSettingsRepository>();
            return services;
        }
    }
}