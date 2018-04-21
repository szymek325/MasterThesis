using DropboxIntegration.User;
using Microsoft.Extensions.DependencyInjection;

namespace DropboxIntegration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDropboxConnector(this IServiceCollection services)
        {
            services.AddTransient<IFilesUploader, FilesUploader>();
            services.AddTransient<IAccountManager, AccountManager>();
            return services;
        }
    }
}