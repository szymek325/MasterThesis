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
            CreateContext(services);
            SeedDb(services);

            services.AddTransient<IDetectionRepository, Repositories.Implementation.DetectionRepository>();
            services.AddTransient<ISensorsReadingRepository, SensorsReadingRepository>();
            services.AddTransient<IDetectionImageRepository, DetectionImageRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<INeuralNetworkRepository, NeuralNetworkRepository>();
            services.AddTransient<IDetectionRepository, Repositories.Implementation.DetectionRepository>();
            services.AddTransient<IRecognitionRepository, RecognitionRepository>();

            return services;
        }

        private static void SeedDb(IServiceCollection services)
        {
            try
            {
                var serviceProvider = services.BuildServiceProvider();
                var context = serviceProvider.GetService<MasterContext>();
                DbInitializer.Seed(context);
            }
            catch (Exception ex)
            {
                
            }
        }

        private static void CreateContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var connectionString = serviceProvider.GetService<IOptions<ConnectionStrings>>();
            services.AddDbContext<MasterContext>(options => options.UseSqlServer(
                connectionString.Value.DefaultConnection,
                optionsBuilder =>
                    optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name)));
        }
    }
}