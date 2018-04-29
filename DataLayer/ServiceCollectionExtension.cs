using System.Reflection;
using DataLayer.Repositories.Implementation;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataLayerModule(this IServiceCollection services)
        {
            services.AddDbContext<MasterContext>(options =>options.UseSqlServer(
                    "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;",
                    optionsBuilder =>optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name)));
            services.AddTransient<IFaceRecognitionJobRepository, FaceRecognitionJobRepository>();
            return services;
        }
    }
}