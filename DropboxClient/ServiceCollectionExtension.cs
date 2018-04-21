using DropboxIntegration.Files;
using DropboxIntegration.User;
using Microsoft.Extensions.DependencyInjection;

namespace DropboxIntegration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDropboxConnector(this IServiceCollection services)
        {
            services.AddTransient<IFilesManager, FilesManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            return services;
        }
    }
}