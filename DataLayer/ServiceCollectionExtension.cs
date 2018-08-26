using DataLayer.Repositories.Implementation;
using DataLayer.Repositories.Interface;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddDbContext<MasterContext>();
            return services;
        }
    }
}