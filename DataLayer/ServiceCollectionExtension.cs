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
            services.AddDbContext<MasterContext>(options => options.UseSqlServer(
                    "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name)));
            //services.AddDbContext<MasterContext>(options => options.UseSqlServer(
            //    "Data Source=den1.mssql6.gear.host;Initial Catalog=masterthesisdb;Integrated Security=False;User ID=masterthesisdb;Password=Zp9P?Q!ezuXH;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
            //    optionsBuilder =>
            //        optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name)));
            services.AddTransient<IFaceDetectionRepository, FaceDetectionRepository>();
            services.AddTransient<ISensorsReadingRepository, SensorsReadingRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<INeuralNetworkRepository, NeuralNetworkRepository>();
            services.AddTransient<IFaceDetectionRepository, FaceDetectionRepository>();
            return services;
        }
    }
}