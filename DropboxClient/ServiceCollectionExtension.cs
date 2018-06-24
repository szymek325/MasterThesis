using DropboxIntegration.Configuration;
using DropboxIntegration.Files;
using DropboxIntegration.Folders;
using DropboxIntegration.Links;
using DropboxIntegration.User;
using Microsoft.Extensions.DependencyInjection;

namespace DropboxIntegration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDropboxConnector(this IServiceCollection services)
        {
            services.AddTransient<IDropboxClientFactory, DropboxClientFactory>();
            services.AddTransient<IFilesClient, FilesClient>();
            services.AddTransient<IFoldersClient, FoldersClient>();
            services.AddTransient<IAccountClient, AccountClient>();
            services.AddTransient<IUrlClient, UrlClient>();
            return services;
        }
    }
}