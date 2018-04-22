using DropboxIntegration.Files;
using DropboxIntegration.Links;
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
            services.AddTransient<IFileUrlProvider, FileUrlProvider>();
            return services;
        }
    }
}